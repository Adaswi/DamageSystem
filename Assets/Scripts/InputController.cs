using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameEvent OnInteraction;
    public GameEvent OnDrop;
    public GameEvent OnHit;
    public GameEvent OnJump;
    public GameEvent<MovementData> OnMove;

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
        OnInteraction?.Raise();
    }

    public void Hit()
    {
        OnHit?.Raise();
    }

    public void Drop()
    {
        OnDrop?.Raise();
    }

    public void Jump()
    {
        OnJump?.Raise();
    }

    public void Move(float horizontal, float vertical)
    {
        var data = new MovementData(horizontal, vertical);
        OnMove?.Raise(data);
    }
}
