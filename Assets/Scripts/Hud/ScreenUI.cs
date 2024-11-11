using UnityEngine;
using UnityEngine.Events;

public class ScreenUI : MonoBehaviour
{
    public UnityEvent OnShowScreen;
    public void ShowScreen()
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnShowScreen?.Invoke();
    }
}
