using System;

public abstract class Consumable<T> : Item
{
    [NonSerialized] public T component;

    public virtual void Consume()
    {
        Destroy(gameObject);
    }
}
