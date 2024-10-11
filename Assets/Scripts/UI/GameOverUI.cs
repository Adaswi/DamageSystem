using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void ShowScreen()
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
