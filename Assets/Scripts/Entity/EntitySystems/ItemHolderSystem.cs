    using UnityEngine;
using UnityEngine.Events;

public class ItemHolderSystem : MonoBehaviour
{
    [SerializeField] private float dropForce;
    [SerializeField] private Item item;
    [SerializeField] private GameObject dropper;
    [SerializeField] private GameObject itemContainer;

    private Rigidbody rb;
    private Collider col;
    private bool isKinematic;
    private bool isTrigger;
    private bool equipped = false;

    public UnityEvent<Item> OnEquipItem;
    public UnityEvent<Item> OnForwardItem;
    public UnityEvent<Item> OnCopyToPOV;
    public UnityEvent OnDropItem;
    public UnityEvent OnUnequipItem;

    private void Awake()
    {
        if (dropper == null)
            dropper = gameObject.transform.parent.gameObject;
    }

    private void Start()
    {
        if (item != null)
            EquipItem(item);
    }

    public void EquipItem(Item item)
    {
        if (!equipped)
        {
            equipped = true;

            this.item = item;
            rb = this.item.GetComponent<Rigidbody>();
            col = this.item.GetComponent<Collider>();

            this.item.transform.SetParent(gameObject.transform);
            this.item.transform.localPosition = Vector3.zero;
            this.item.transform.localRotation = Quaternion.Euler(Vector3.zero);

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

            OnEquipItem?.Invoke(item);
        }
    }

    public void UnequipItem()
    {
        item = null;
        isKinematic = false;
        isTrigger = false;
        equipped = false;

        OnUnequipItem?.Invoke();
    }

    public void DropItem()
    {
        if (!equipped)
            return;

        if (itemContainer == null)
            item.transform.SetParent(null);
        else
            item.transform.SetParent(itemContainer.transform);

        item.transform.position = dropper.transform.position;
        item.transform.rotation = Quaternion.identity;

        var dropperRb = dropper.GetComponent<Rigidbody>();

        if (col != null)
            col.isTrigger = isTrigger;

        if (rb != null)
        {
            rb.isKinematic = isKinematic;
            rb.AddForce(dropper.transform.forward * dropForce * rb.mass, ForceMode.Impulse);
        }

            if (dropperRb != null && rb != null)
                rb.velocity = dropperRb.velocity;

        UnequipItem();

        OnDropItem?.Invoke();
    }

    public void ForwardItem()
    {
        if (equipped && item.IsStorable)
        {
            OnForwardItem?.Invoke(item);
            UnequipItem();
        }
    }

    public void CopyItemToPOV()
    {
        if (equipped)
        {
            var newItem = Instantiate(item.gameObject);
            OnCopyToPOV?.Invoke(newItem.GetComponent<Item>());
        }
    }

    public void DeleteItem()
    {
        if (equipped)
        {
            Destroy(item.gameObject);
            UnequipItem();
        }
    }
}
