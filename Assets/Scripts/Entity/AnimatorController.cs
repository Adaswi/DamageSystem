using UnityEngine;

public class AnimatorController : EntitySystem
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        
        entityID.events.OnDeath += Disable;
        entityID.events.OnHit0arg += PlayHit;
    }

    public void OnDisable()
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }

        entityID.events.OnDeath -= Disable;
        entityID.events.OnHit0arg -= PlayHit;
    }

    protected override void Awake()
    {
        base.Awake();
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void PlayHit()
    {
        animator.SetTrigger("IsHit");
    }

    public void Disable()
    {
        animator.enabled = false;
        enabled = false;
    }
}
