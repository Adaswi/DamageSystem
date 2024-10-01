using UnityEngine;
using UnityEngine.Events;

public class PlayerPickUpController : MonoBehaviour
{
    [SerializeField] private float pickUpRange;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform playerCam;
    private RaycastHit hit;

    public UnityEvent<GameObject> OnPickUp;

    public void PickUp()
    {
        if(Physics.Raycast(new Ray(playerCam.transform.position, playerCam.transform.forward), out hit, pickUpRange, mask))
        {
            OnPickUp?.Invoke(hit.collider.gameObject);
        }
    }
}
