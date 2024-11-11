using UnityEngine;
using UnityEngine.Events;

public class PlayerConsume<Potion, Component> : MonoBehaviour
{
    [SerializeField] protected Component component;
    protected Consumable<Component> consumable;
    protected TimedConsumable<Component> instance;
    protected bool isReady;

    public Component Comp { get { return component; } set { component = value; } }
    public UnityEvent OnConsume;
    public UnityEvent<float> OnConsumeTimed;
    public UnityEvent OnRemoveInstance;

    public void ReadyToConsume(Item item)
    {
        consumable = item.GetComponent<Potion>() as Consumable<Component>;
        if (consumable != null)
        {
            consumable.component = this.component;
            isReady = true;
        }
    }

    public void UnreadyToConsume()
    {
        consumable = null;
        isReady = false;
    }

    public void Consume()
    {
        if (!isReady)
            return;
        if (instance != null)
            instance.RemoveEffect();
        instance = consumable as TimedConsumable<Component>;
        if (instance != null)
        {
            instance.OnRemoveEffect += RemoveInstance;
            instance.Consume();
            OnConsumeTimed?.Invoke(instance.Duration);

        }
        else
            consumable.Consume();

        UnreadyToConsume();
        OnConsume?.Invoke();
    }

    private void RemoveInstance()
    {
        instance = null;
        OnRemoveInstance?.Invoke();
    }
}
