using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public bool isGrounded;

    private void OnTriggerStay()
    {
            isGrounded = true;
    }

    private void OnTriggerExit()
    {
            isGrounded = false;
    }
}
