using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform playerPosition;
    public Transform playerRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = playerPosition.position;
        temp[1] += 0.5f; //move camera 0.5 above player position
        transform.position = temp;

        transform.rotation = playerRotation.rotation;
    }
}
