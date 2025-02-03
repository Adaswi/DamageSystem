using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{
    private Weapon weapon;

    public Weapon Weapon {
        get { return weapon; }
        set { 
            weapon = value;
            IsReady = true;
            OnAttackReady?.Invoke();
        }
    }
    public bool IsReady { get; private set; }
    public bool IsAttacking { get; private set; }

    public UnityEvent OnAttackEnter;
    public UnityEvent<Weapon> OnAttack;
    public UnityEvent OnAttackExit;
    public UnityEvent<Weapon> OnMiss;
    public UnityEvent OnAttackReady;
    public UnityEvent OnAttackUnready;

    //Ready to attack when weapon is equipped
    public void ReadyToAttack(Item item)
    {
        var weapon = item.GetComponent<Weapon>();
        if (weapon == null)
            return;

        this.weapon = weapon;
        IsReady = true;
        OnAttackReady?.Invoke();
    }

    public void UnreadyToAttack()
    {
        this.Weapon = null;
        IsReady = false;
        OnAttackUnready?.Invoke();
    }

    public void Attack(IBodypart bodypart)
    {
        if (IsAttacking || !IsReady)
            return;
        Enter();
        bodypart.Hit(Weapon.Attack, Weapon.Effects);
        OnAttack?.Invoke(Weapon);
        Invoke(nameof(Exit), Weapon.Speed);
    }

    public void Miss()
    {
        if (IsAttacking || !IsReady)
            return;
        Enter();
        OnMiss?.Invoke(Weapon);
        Invoke(nameof(Exit), Weapon.Speed);
    }

    private void Enter()
    {
        IsAttacking = true;
        OnAttackEnter?.Invoke();
    }

    private void Exit()
    {
        IsAttacking = false;
        OnAttackExit?.Invoke();
    }
}
