using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnGameStart;

    private void Start()
    {
        OnGameStart?.Invoke();
    }

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
