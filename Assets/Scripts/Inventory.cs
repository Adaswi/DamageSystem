using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemsUI itemsUI;
    [SerializeField] private IntData slotsCount;
    [SerializeField] private Transform player;
    private GameObject[] inventory;

    public UnityEvent OnInventoryFull;
    public UnityEvent<GameObject> OnRemoveItem;
    public UnityEvent<GameObject> OnAddItem;

    private void Awake()
    {
        inventory = new GameObject[slotsCount.value];
    }

    public void AddItem(GameObject item)
    {
        var index = 0;
        while (index < inventory.Length && inventory[index] != null)
        {
            index++;
        }

        if (inventory[index] == null)
        {
            inventory[index] = item;
            item.transform.SetParent(itemsUI.GenerateItem(item, index).transform, false);
            item.transform.localPosition = Vector3.zero;
            item.SetActive(false);
            OnAddItem?.Invoke(item);
        }
        else
        {
            InventoryFull();
        }
    }

    public void RemoveItem(int index)
    {
        var temp = inventory[index];
        inventory[index] = null;
        temp.transform.SetParent(null);
        var rb = temp.GetComponent<Rigidbody>();
        var col = temp.GetComponent<Collider>();

        if (col != null)
            col.isTrigger = false;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(player.transform.forward * 1 * rb.mass, ForceMode.Impulse);
        }

        OnRemoveItem?.Invoke(temp);
    }

    public void InventoryFull()
    {
        OnInventoryFull?.Invoke();
    }
}
