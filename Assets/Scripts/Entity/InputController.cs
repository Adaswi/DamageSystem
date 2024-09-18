using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    public UnityEvent OnInteraction;
    public UnityEvent OnDrop;
    public UnityEvent OnHit;
    public UnityEvent OnJump;
    public UnityEvent<MovementData> OnMove;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Interaction();
        
        if (Input.GetKeyDown(KeyCode.Q))
            Drop();
        
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        
        if (Input.GetMouseButtonDown(0))
            Hit();

        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public void Interaction()
    {
        OnInteraction?.Invoke();
    }

    public void Hit()
    {
        OnHit?.Invoke();
    }

    public void Drop()
    {
        OnDrop?.Invoke();
    }

    public void Jump()
    {
        OnJump?.Invoke();
    }

    public void Move(float horizontal, float vertical)
    {
        var data = new MovementData(horizontal, vertical);
        OnMove?.Invoke(data);
    }
}
