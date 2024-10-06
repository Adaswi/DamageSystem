using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int FrameIndex;
    public Action OnDropButtonClicked;
    public Action OnUseButtonClicked;

    public Item(int frameIndex)
    {
        this.FrameIndex = frameIndex;
    }

    public void DropButtonClicked()
    {
        OnDropButtonClicked?.Invoke();
    }

    public void UseButtonClicked()
    {
        OnUseButtonClicked?.Invoke();
    }
}
