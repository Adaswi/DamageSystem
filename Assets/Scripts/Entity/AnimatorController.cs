using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody[] rigidbodies;

    private void OnEnable()
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }

    public void OnDisable()
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    private void Awake()
    {
        if (rigidbodies == null)
            rigidbodies = GetComponentsInChildren<Rigidbody>();
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void PlayHit()
    {
        animator.SetTrigger("IsHit");
    }

    public void PlayWalk(MovementData data)
    {
        animator.SetFloat("IsWalkingVertical", data.vertical);
        animator.SetFloat("IsWalkingHorizontal", data.horizontal);
    }

    public void PlayAttack(Weapon weapon)
    {
        animator.SetFloat("AttackSpeed", animator.speed/weapon.Speed);
        if(weapon.IsPiercing)
            animator.SetTrigger("IsPierceAttacking");
        else
            animator.SetTrigger("IsSwingAttacking");
    }

    public void PlayAttack(IBodypart bodypart, Weapon weapon)
    {
        animator.SetFloat("AttackSpeed", animator.speed / weapon.Speed);
        if (weapon.IsPiercing)
            animator.SetTrigger("IsPierceAttacking");
        else
            animator.SetTrigger("IsSwingAttacking");
    }

    public void ResetAttackTrigger()
    {
        Debug.Log("ResetAttackTrigger");
        animator.ResetTrigger("IsPierceAttacking");
        animator.ResetTrigger("IsSwingAttacking");
    }

    public void Disable()
    {
        animator.enabled = false;
        enabled = false;
    }
}
