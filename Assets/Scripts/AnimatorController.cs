using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (animator == null)
        animator = GetComponent<Animator>();
    }

    public void Hit()
    {
        animator.SetTrigger("IsHit");
    }

    public void TurnOn()
    {
        animator.enabled = true;
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }

    public void TurnOff()
    {
        animator.enabled = false;
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }
}
