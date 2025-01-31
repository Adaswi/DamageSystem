using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int count = 0;
    public int MaxCount;

    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            if (count == MaxCount)
            {
                WinConditionMet();
            }
        }
    }

    public UnityEvent OnGameStart;
    public UnityEvent OnWinConditionMet;


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


    public void IncrementCount()
    {
        Count++;
    }

    public void WinConditionMet()
    {
        OnWinConditionMet?.Invoke();
    }
}
