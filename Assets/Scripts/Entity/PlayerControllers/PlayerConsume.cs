using UnityEngine;
using UnityEngine.Events;

public class PlayerConsume<Potion, Component> : MonoBehaviour
{
    [SerializeField] public Component Comp {  get; set; }
    private Consumable<Component> consumable;
    private TimedConsumable<Component> instance;
    private bool isReady;

    public UnityEvent OnConsume;
    public UnityEvent<float> OnConsumeTimed;

    public void ReadyToConsume(GameObject item)
    {
        consumable = item.GetComponent<Potion>() as Consumable<Component>;
        if (consumable != null)
        {
            consumable.component = this.Comp;
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
    }
}
