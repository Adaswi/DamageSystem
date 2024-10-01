using UnityEngine;
using UnityEngine.Events;

public class AttackSystem : MonoBehaviour
{
    private bool isAttacking;

    public UnityEvent OnAttackEnter;
    public UnityEvent OnAttack;
    public UnityEvent OnAttackExit;

    private void Enter()
    {
        isAttacking = true;
        OnAttackEnter?.Invoke();
    }
    public void Attack(Bodypart bodypart, Weapon weapon)
    {
        if (isAttacking)
            return;
        Enter();
        bodypart.Hit(weapon.attack.value, weapon.effects.values);
        OnAttack?.Invoke();
        Invoke(nameof(Exit), weapon.speed.value);
    }

    private void Exit()
    {
        isAttacking = false;
        OnAttackExit?.Invoke();
    }
}
