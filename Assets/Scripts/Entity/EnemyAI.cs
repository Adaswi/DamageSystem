using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player, enemy;
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask bodypartMask, playerMask;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    [SerializeField] private CustomNavMeshAgent agent;
    [SerializeField] private Vector3[] patrolDestinations;
    [SerializeField] private float[] patrolRoatations;
    [SerializeField] private float[] patrolWaitTimes;

    private Weapon weapon;
    private bool needsDirection = true;
    private int randomDirection = 1;
    private int patrolStage = 0;
    private bool playerDetected;
    private bool setAnotherDestination;
    private bool isWaiting;
    private bool attakcEnabled;

    public bool AttackEnabled
    {
        get { return attakcEnabled; }
        set { attakcEnabled = value; }
    }


    public UnityEvent<MovementData> OnMovement;
    public UnityEvent OnAttackPlayer;


    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (col == null)
            col = GetComponent<Collider>();

        if (patrolRoatations.Length != patrolDestinations.Length)
            Array.Resize(ref patrolRoatations, patrolDestinations.Length);

        if (patrolWaitTimes.Length != patrolDestinations.Length)
            Array.Resize(ref patrolWaitTimes, patrolDestinations.Length);
    }

    public void SetWeapon(Item item)
    {
        var component = item.GetComponent<Weapon>();
        if (weapon == null && component)
            weapon = component;
    }

    public void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    public void CirclePlayer()
    {
        agent.ReachDestination();
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        rotation.x = 0;
        rotation.z = 0;
        rb.MoveRotation(rotation.normalized);
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
        OnAttackPlayer?.Invoke();
    }

    public void Patrol(Vector3[] destinations, float[] rotations, float[] waitTimes)
    {
        Debug.Log(gameObject.name + " patrol stage is " + patrolStage);

        if (destinations.Length == 0)
        {
            OnMovement?.Invoke(new MovementData(0, 0));
            return;
        }

        if (patrolStage >= destinations.Length)
            patrolStage = 0;

        if (setAnotherDestination)
        {
            agent.SetDestination(destinations[patrolStage]);
            setAnotherDestination = false;
            Debug.Log(gameObject.name + " destination set.");
        }

        if (!agent.IsDestinationSet)
        {
            Debug.Log(gameObject.name + " is at the destination or can't reach the destination.");
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.x, rotations[patrolStage], transform.rotation.z), Time.deltaTime* 90));
            if (!isWaiting)
            {
                isWaiting = true;
                Invoke(nameof(IncrementPatrolStage), waitTimes[patrolStage]);
            }
        }
    }

    private void IncrementPatrolStage()
    {
        setAnotherDestination = true;
        patrolStage++;
        isWaiting = false;
    }

    public void GetWeapon(Item item)
    {
        var weapon = item.GetComponent<Weapon>();
        if (weapon != null)
            this.weapon = weapon;
    }

    public void RemoveWeapon()
    {
        weapon = null;
    }

    private void Update()
    {
        RaycastHit raycastCheck;
        if (Physics.CheckBox(enemy.position + enemy.forward * detectionRange / 1.75f, new Vector3(detectionRange / 2, detectionRange / 2, detectionRange), Quaternion.identity, playerMask) && Physics.Linecast(enemy.position, player.position, out raycastCheck))
            if (1 << raycastCheck.transform.gameObject.layer == playerMask.value)
                playerDetected = true;

        if (Vector3.Distance(enemy.position, player.position) > detectionRange*1.5)
            playerDetected = false;

        if (!playerDetected)
            Patrol(patrolDestinations, patrolRoatations, patrolWaitTimes);
        else if (!attakcEnabled && Physics.CheckBox(enemy.position + enemy.forward * weapon.Range / 2, new Vector3(weapon.Range, weapon.Range, weapon.Range / 2), enemy.rotation, playerMask) && Physics.CheckSphere(enemy.position, weapon.Range, playerMask))
            AttackPlayer();
        else if (Physics.CheckSphere(enemy.position, weapon.Range*0.75f, playerMask) && Physics.CheckBox(enemy.position + enemy.forward * weapon.Range / 2, new Vector3(weapon.Range, weapon.Range, weapon.Range / 2), enemy.rotation, playerMask))
            CirclePlayer();
        else
            ChasePlayer();
    }
}
