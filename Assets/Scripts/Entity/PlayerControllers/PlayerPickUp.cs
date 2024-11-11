using UnityEngine;
using UnityEngine.Events;

public class PlayerPickUpController : MonoBehaviour
{
    [SerializeField] private float pickUpRange;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform playerCam;
    private RaycastHit hit;

    public UnityEvent<Item> OnPickUp;
    public UnityEvent<Item> OnStore;

    public void PickUp()
    {
        if(Physics.Raycast(new Ray(playerCam.transform.position, playerCam.transform.forward), out hit, pickUpRange, mask) && hit.transform.gameObject.GetComponentInParent<ItemHolderSystem>() == null)
        {
            var item = hit.transform.GetComponent<Item>();
            if (!item)
                return;

            if(item.IsPickUpable)
            {
                OnPickUp?.Invoke(item);
            }
            else if(item.IsStorable)
            {
                OnStore?.Invoke(item);
            }

        }
    }
}
