using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform playerPosition;
    public Transform playerRotation;

    // This class bounds camera postion to player's position (just putting it as a child can create stutter problems)
    void Update()
    {
        transform.position = playerPosition.position;
        transform.rotation = playerRotation.rotation;
    }
}
