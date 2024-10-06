using System;
using UnityEngine;

public abstract class Consumable<T> : MonoBehaviour
{
    [NonSerialized] public T component;

    public virtual void Consume()
    {
        Destroy(gameObject);
    }
}
