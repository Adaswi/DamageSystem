using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player, enemy;
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask bodypartMask, playerMask;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Vector3[] patrolDestinations;
    [SerializeField] private float[] patrolRoatations;
    [SerializeField] private float[] patrolWaitTimes;

    private Weapon weapon;
    private bool isDead;
    private bool isAttacking;
    private bool needsDirection = true;
    private int randomDirection = 1;
    private int patrolStage = 0;
    private bool playerDetected;
    private bool isDestinationSet;
    private bool isWaiting;

    public UnityEvent<MovementData> OnMovement;
    public UnityEvent<Bodypart, Weapon> OnAttack;

    public bool IsAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (col == null)
            col = GetComponent<Collider>();

        navMeshAgent.updatePosition = false;

        if (patrolRoatations.Length != patrolDestinations.Length)
            Array.Resize(ref patrolRoatations, patrolDestinations.Length);

        if (patrolWaitTimes.Length != patrolDestinations.Length)
            Array.Resize(ref patrolWaitTimes, patrolDestinations.Length);
    }

    public void ChasePlayer()
    {
        navMeshAgent.nextPosition = enemy.position;
        navMeshAgent.SetDestination(player.position);
        if (navMeshAgent.pathEndPosition == navMeshAgent.destination)
            OnMovement.Invoke(new MovementData(0,1));
        else
            OnMovement.Invoke(new MovementData(0, 0));
    }

    public void CirclePlayer()
    {
        var rotation = Quaternion.LookRotation(player.position - transform.position);
        rotation.x = 0;
        rotation.z = 0;
        rb.MoveRotation(rotation);
        if (needsDirection)
        {
            needsDirection = false;
            Invoke(nameof(GetRandomDirection), UnityEngine.Random.Range(0.5f, 2));
        }
        OnMovement.Invoke(new MovementData(randomDirection, 0));
    }

    private void GetRandomDirection()
    {
        var values = new int[2] {-1, 1};
        randomDirection = values[UnityEngine.Random.Range(0, values.Length)];
        needsDirection = true;
    }

    public void AttackPlayer()
    {
        if (weapon == null)
            return;
        var objects = Physics.OverlapSphere(transform.position, weapon.range.value, bodypartMask);
        var bodyparts = new List<Bodypart>();
        foreach (var obj in objects)
        {
            var bodypart = obj.GetComponent<Bodypart>();
            if (bodypart != null)
                bodyparts.Add(bodypart);
        }
        var index = UnityEngine.Random.Range(0, bodyparts.Count);

        if (!bodyparts.Any() || bodyparts[index] == null)
            return;

        OnAttack?.Invoke(bodyparts[index], weapon);
    }

    /*
    public void Patrol(Vector3[] stages)
    {
        navMeshAgent.nextPosition = enemy.position;

        if (stages.Length > 1)
            isDestinationSet = false;
        else if (stages.Length == 1 && isDestinationSet)
        {
            navMeshAgent.SetDestination(stages[0]);
            isDestinationSet = true;
        }
        else if (stages.Length == 0)
        {
            return;
        }

        if (patrolStage > stages.Length - 1)
            patrolStage = 0;


        if (!isDestinationSet)
        {
            navMeshAgent.SetDestination(stages[patrolStage]);
            isDestinationSet = true;
        }
        else if (navMeshAgent.remainingDistance < 0.6f)
        {
            OnMovement?.Invoke(new MovementData(0, 0));
            patrolStage++;
        }
        else if (navMeshAgent.pathEndPosition == navMeshAgent.destination)
        {
            OnMovement?.Invoke(new MovementData(0, 1));
        }
        else
        {
            OnMovement?.Invoke(new MovementData(0, 0));
        }
    }
    */

    public void Patrol(Vector3[] destinations, float[] rotations, float[] waitTimes)
    {
        navMeshAgent.nextPosition = enemy.position;
        Debug.Log(gameObject.name + " patrol stage is " + patrolStage);

        if (destinations.Length == 0)
        {
            OnMovement?.Invoke(new MovementData(0, 0));
            return;
        }

        if (patrolStage >= destinations.Length)
            patrolStage = 0;

        if (!isDestinationSet)
        {
            Debug.Log(gameObject.name + " destination set.");
            navMeshAgent.SetDestination(destinations[patrolStage]);
            isDestinationSet = true;
        }

        if (navMeshAgent.remainingDistance < 0.5f || navMeshAgent.pathEndPosition != navMeshAgent.destination)
        {
            Debug.Log(gameObject.name + " is at the destination or can't reach the destination.");
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.x, rotations[patrolStage], transform.rotation.z), 5);
            OnMovement?.Invoke(new MovementData(0, 0));
            if (!isWaiting)
            {
                isWaiting = true;
                Invoke(nameof(IncrementPatrolStage), waitTimes[patrolStage]);
            }
        }
        else
        {
            Debug.Log(gameObject.name + " is walking.");
            OnMovement?.Invoke(new MovementData(0, 1));
        }
    }

    private void IncrementPatrolStage()
    {
        isDestinationSet = false;
        patrolStage++;
        isWaiting = false;
    }

    public void GetWeapon(GameObject item)
    {
        var weapon = item.GetComponent<Weapon>();
        if (weapon != null)
            this.weapon = weapon;
    }

    public void RemoveWeapon()
    {
        weapon = null;
    }

    public void AttackExit()
    {
        isAttacking = false;
    }

    public void Death()
    {
        isDead = true;        
    }

    private void Update()
    {
        if (isDead)
            return;

        RaycastHit raycastCheck;
        if (Physics.CheckBox(enemy.position + enemy.forward * detectionRange / 1.75f, new Vector3(detectionRange / 2, detectionRange / 2, detectionRange), Quaternion.identity, playerMask) && Physics.Linecast(enemy.position, player.position, out raycastCheck))
            if (1 << raycastCheck.transform.gameObject.layer == playerMask.value)
                playerDetected = true;

        if (Vector3.Distance(enemy.position, player.position) > detectionRange*1.5)
            playerDetected = false;

        if (!playerDetected)
            Patrol(patrolDestinations, patrolRoatations, patrolWaitTimes);
        else if (weapon != null && !isAttacking && Physics.CheckBox(enemy.position + enemy.forward * weapon.range.value / 2, new Vector3(weapon.range.value, weapon.range.value, weapon.range.value / 2), enemy.rotation, playerMask) && Physics.CheckSphere(enemy.position, weapon.range.value, playerMask))
            AttackPlayer();
        else if (Physics.CheckSphere(enemy.position, weapon.range.value / 1.5f, playerMask) && Physics.CheckBox(enemy.position + enemy.forward * weapon.range.value / 2, new Vector3(weapon.range.value, weapon.range.value, weapon.range.value / 2), enemy.rotation, playerMask))
            CirclePlayer();
        else
            ChasePlayer();
    }
}
