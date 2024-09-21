using System;
using UnityEngine;

public abstract class TimedConsumable<T> : Consumable<T>
{
    [SerializeField] protected FloatData duration;

    public Action OnRemoveEffect;

    public override void Consume()
    {
        Invoke(nameof(RemoveEffect), duration.value);
        Debug.Log("Timed potion consumed");
        gameObject.SetActive(false);
    }

    public virtual void RemoveEffect()
    {
        Debug.Log("Effect removed");
        OnRemoveEffect?.Invoke();
        Destroy(gameObject);
    }
}