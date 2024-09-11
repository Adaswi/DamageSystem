using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    [SerializeField] private List<Collider> colliders = new List<Collider>();
    [SerializeField] private bool isGrounded = false;

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
                OnGroundedChange?.Invoke();
            }
        }
    }

    public Action OnGroundedChange;

    private void OnTriggerEnter(Collider other)
    {
        colliders.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
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
