using Assets.Scripts.ScriptableObjects;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] private float dropForce;
    private bool equipped = false;

    private GameObject item;
    private Rigidbody rb;
    private Collider col;
    private bool isKinematic;
    private bool isTrigger;

    public GameEvent<GameObject> OnEquipped;
    public GameEvent<GameObject> OnDropped;

    public void ItemEquip(GameObject item_)
    {
        if (!equipped)
        {
            equipped = true;

            item = item_;
            rb = item.GetComponent<Rigidbody>();
            col = item.GetComponent<Collider>();

            item.transform.SetParent(gameObject.transform);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.Euler(Vector3.zero);

            if (rb != null)
            {
                isKinematic = rb.isKinematic;
                rb.isKinematic = true;
            }

            if (col != null)
            {
                isTrigger = col.isTrigger;
                col.isTrigger = true;
            }

            OnEquipped.Raise(item);
        }
    }

    public void ItemDropped(GameObject dropper)
    {
        if (equipped)
        {
            item.transform.SetParent(null);
            item.transform.position = dropper.transform.position;

            var dropperRb = dropper.GetComponent<Rigidbody>();
           
            if (dropperRb != null && rb != null)
            rb.velocity = dropper.GetComponent<Rigidbody>().velocity;

            if(col != null)
                col.isTrigger = isTrigger;

            if (rb != null)
            {
                rb.isKinematic = isKinematic;
                rb.AddForce(transform.forward * dropForce * rb.mass, ForceMode.Impulse);
            }

            equipped = false;
            OnDropped.Raise(item);
        }
    }
}
