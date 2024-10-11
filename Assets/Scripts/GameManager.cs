using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
