using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackSystem : MonoBehaviour
{
    private bool isAttacking;

    public UnityEvent OnAttackEnter;
    public UnityEvent OnAttack;
    public UnityEvent OnAttackExit;
    public UnityEvent OnMiss;

    private void Enter()
    {
        isAttacking = true;
        OnAttackEnter?.Invoke();
    }
    public void Attack(IBodypart bodypart, Weapon weapon)
    {
        if (isAttacking)
            return;
        Enter();
        bodypart.Hit(weapon.Attack, weapon.Effects);
        OnAttack?.Invoke();
        Invoke(nameof(Exit), weapon.Speed);
    }

    public void Miss(Weapon weapon)
    {
        if (isAttacking)
            return;
        Enter();
        OnMiss?.Invoke();
        Invoke(nameof(Exit), weapon.Speed);
    }

    private void Exit()
    {
        isAttacking = false;
        OnAttackExit?.Invoke();
    }
}
