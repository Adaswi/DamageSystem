using UnityEngine;
using UnityEngine.Events;

public class InventoryHud : MonoBehaviour
{
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
}
