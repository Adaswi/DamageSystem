using UnityEngine;

public class ItemHolder : EntitySystem
{
    [SerializeField] private float dropForce;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject dropper;

    private Rigidbody rb;
    private Collider col;
    private bool isKinematic;
    private bool isTrigger;
    private bool equipped = false;

    public GameEvent<GameObject> OnEquipped;
    public GameEvent OnDropped;

    protected override void Awake()
    {
        base.Awake();
        if (dropper == null)
            dropper = gameObject.transform.parent.gameObject;
    }

    private void Start()
    {
        if (item != null)
            ItemEquip(item);
    }

    private void OnEnable()
    {
        entityID.events.OnDeath += ItemDropped;
    }

    public void ItemEquip(GameObject item_)
    {
        if (!equipped && item_.GetComponentInParent<ItemHolder>() == null)
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

            entityID.events.OnItemEquip?.Invoke();
            OnEquipped?.Raise(item);
        }
    }

    public void ItemDropped()
    {
        if (equipped)
        {
            item.transform.SetParent(null);
            item.transform.position = dropper.transform.position;

            var dropperRb = dropper.GetComponent<Rigidbody>();

            if (col != null)
                col.isTrigger = isTrigger;

            if (rb != null)
            {
                rb.isKinematic = isKinematic;
                rb.AddForce(transform.forward * dropForce * rb.mass, ForceMode.Impulse);
            }

            if (dropperRb != null && rb != null)
                rb.velocity = dropperRb.velocity;

            equipped = false;

            entityID.events.OnItemDrop?.Invoke();
            OnDropped?.Raise();
        }
    }
}
