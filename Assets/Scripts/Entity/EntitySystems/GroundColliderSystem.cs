using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundColliderSystem : MonoBehaviour
{
    [SerializeField] private List<Collider> colliders = new List<Collider>();
    [SerializeField] private bool isGrounded = false;

    public LayerMask groundMask;

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        set
        {
            if (isGrounded != value)
            {
                isGrounded = value;
                OnGroundedChange?.Invoke(isGrounded);
            }
        }
    }

    public UnityEvent<bool> OnGroundedChange;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & groundMask) != 0)
            colliders.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & groundMask) != 0)
            colliders.Remove(other);
    }

    private void Update()
    {
        if (colliders.Count == 0)
            IsGrounded = false;
        else
            IsGrounded = true;
    }
}
