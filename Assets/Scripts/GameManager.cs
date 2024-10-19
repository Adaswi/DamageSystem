using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float tempTime = 1;
    public void Pause()
    {
        tempTime = Time.timeScale;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = tempTime;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
