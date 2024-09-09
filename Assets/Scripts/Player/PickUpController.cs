using Assets.Scripts.ScriptableObjects;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField] private float pickUpRange;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform playerCam;
    private RaycastHit hit;

    public GameEvent<GameObject> OnPickUp;

    public void PickUp()
    {
        if(Physics.Raycast(new Ray(playerCam.transform.position, playerCam.transform.forward), out hit, pickUpRange, mask))
        {
            OnPickUp.Raise(hit.collider.gameObject);
        }
    }
}
