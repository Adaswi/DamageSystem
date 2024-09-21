using UnityEngine;

public abstract class Consumable<T> : MonoBehaviour
{
    public T component;

    public virtual void Consume()
    {
        Destroy(gameObject);
    }
}
