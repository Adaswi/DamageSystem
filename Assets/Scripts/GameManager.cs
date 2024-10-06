using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float tempTime;
    public void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = tempTime;
        }
        else
        {
            tempTime = Time.timeScale;
            Time.timeScale = 0;
        }
    }
}
