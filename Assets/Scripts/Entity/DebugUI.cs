using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField]private TMP_Text textComponent;
    [SerializeField]private Rigidbody rb;
    [SerializeField] private Rigidbody enemyRb;

    private void Update()
    {
        textComponent.text = "Speed: " + rb.velocity.magnitude.ToString() + "\n" +
            "Enemy speed: " + enemyRb.velocity.magnitude.ToString();
    }
}
