using UnityEngine;
using UnityEngine.Events;

public class WinCondition : MonoBehaviour
{
    public int count = 0;
    public int maxCount;

    public UnityEvent OnWinConditionMet;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            if (count == maxCount)
            {
                WinConditionMet();
            }
        }
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
