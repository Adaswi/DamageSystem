using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player, enemy;
    [SerializeField] private float smooth;
    [SerializeField] private LayerMask bodypartMask, playerMask;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    [SerializeField] private NavMeshAgent navMeshAgent;

    private Weapon weapon;
    private bool isDead;
    private bool isAttacking;
    private bool needsDirection = true;
    private int randomDirection = 1;

    public UnityEvent<MovementData> OnMovement;
    public UnityEvent OnAttack;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (col == null)
            col = GetComponent<Collider>();
    }

    public void ChasePlayer()
    {
        /*
        var rotation = Quaternion.LookRotation(player.position - transform.position);
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);
        */
        navMeshAgent.updatePosition = false;
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
        transform.rotation = rotation;
        if (needsDirection)
        {
            needsDirection = false;
            Invoke(nameof(GetRandomDirection), Random.Range(0.5f, 2));
        }
        OnMovement.Invoke(new MovementData(randomDirection, 0));
    }

    public void GetRandomDirection()
    {
        var values = new int[2] {-1, 1};
        randomDirection = values[Random.Range(0, values.Length)];
        needsDirection = true;
    }

    public void AttackPlayer()
    {
        if (weapon == null)
            return;
        isAttacking = true;
        var objects = Physics.OverlapSphere(transform.position, weapon.range.value, bodypartMask);
        var bodyparts = new List<Bodypart>();
        foreach (var obj in objects)
        {
            var bodypart = obj.GetComponent<Bodypart>();
            if (bodypart != null)
                bodyparts.Add(bodypart);
        }
        var index = Random.Range(0, bodyparts.Count);

        if (bodyparts[index] == null)
            return;

        bodyparts[index].Hit(weapon.attack.value, weapon.effects.values);
        OnAttack?.Invoke();
        Invoke(nameof(AttackExit), weapon.speed.value);

        Debug.Log("Attack player executed");
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
        if (weapon != null && Physics.CheckSphere(enemy.position, weapon.range.value, playerMask) && Physics.CheckBox(enemy.position+enemy.forward*weapon.range.value/2, new Vector3(weapon.range.value, weapon.range.value, weapon.range.value/2),enemy.rotation, playerMask ) && !isAttacking)
        {
            AttackPlayer();
        }
        else if (Physics.CheckSphere(enemy.position, weapon.range.value/1.5f, playerMask) && Physics.CheckBox(enemy.position+enemy.forward*weapon.range.value/2, new Vector3(weapon.range.value, weapon.range.value, weapon.range.value/2),enemy.rotation, playerMask ))
        {
            CirclePlayer();
        }
        else
        {
            ChasePlayer();
        }
    }
}
