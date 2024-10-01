using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform playerPosition;
    public Transform playerRotation;

    // This class bounds camera postion to player's position (just putting it as a child can create stutter problems)
    void Update()
    {
        Vector3 temp = playerPosition.position;
        temp[1] += 1.5f; //move camera 1.5 above player position
        transform.position = temp;

        transform.rotation = playerRotation.rotation;
    }
}
