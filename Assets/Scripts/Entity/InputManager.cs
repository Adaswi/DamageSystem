using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent OnInteraction;
    public UnityEvent OnDrop;
    public UnityEvent OnUse;
    public UnityEvent OnJump;
    public UnityEvent OnInventory;
    public UnityEvent<MovementData> OnMove;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Interact();
        
        if (Input.GetKeyDown(KeyCode.Q))
            Drop();
        
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        
        if (Input.GetMouseButtonDown(0))
            Use();

        if (Input.GetKeyDown(KeyCode.Tab))
            Inventory();

        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public void Interact()
    {
        OnInteraction?.Invoke();
    }

    public void Use()
    {
        OnUse?.Invoke();
    }

    public void Drop()
    {
        OnDrop?.Invoke();
    }

    public void Jump()
    {
        OnJump?.Invoke();
    }

    public void Inventory()
    {
        OnInventory?.Invoke();
    }

    public void Move(float horizontal, float vertical)
    {
        var data = new MovementData(horizontal, vertical);
        OnMove?.Invoke(data);
    }
}
