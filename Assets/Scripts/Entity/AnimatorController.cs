using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }

    public void OnDisable()
    {
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    private void Awake()
    {
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
