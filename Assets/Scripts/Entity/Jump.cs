using UnityEngine;
using UnityEngine.Events;

public class JumpDetection : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpCooldown;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private GroundCollider gc;

    public bool isJumping;
    public UnityEvent OnJump;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (gc == null)
            gc = GetComponent<GroundCollider>();
    }

    public void Jump()
    {
        if (!isJumping && gc.IsGrounded)
        {
            isJumping = true;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            OnJump?.Invoke();
            Invoke(nameof(ExitJump), jumpCooldown);
        }
    }

    private void ExitJump()
    {
        isJumping = false;
    }
}
