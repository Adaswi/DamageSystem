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
}
