using UnityEngine;
using UnityEngine.Events;

public class InventoryHud : MonoBehaviour
{
    [SerializeField] private IntData slotsCount;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject containerObject;
    private GameObject[] itemFrames;

    public UnityEvent OnInventoryOpen;
    public UnityEvent OnInventoryClose;
    public UnityEvent<int, Item> OnStoreItem;
    public void ToggleInventory()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            OnInventoryOpen?.Invoke();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            OnInventoryClose?.Invoke();
        }
    }

    private void Awake()
    {
        itemFrames = new GameObject[slotsCount.value];
        for (int i = 0; i < slotsCount.value; i++)
        {
            itemFrames[i] = Instantiate(slotPrefab);
            itemFrames[i].transform.SetParent(containerObject.transform, false);
        }
    }

    public void StoreItem(Item item)
    {
        var i = 0;
        Debug.Log(item);
        foreach (GameObject frame in itemFrames)
        {
            if (frame.transform.childCount > 0)
            {
                i++;
            }
            else
            {
                OnStoreItem?.Invoke(i, item);
                Destroy(item.gameObject);
                break;
            }
        }
    }
}
