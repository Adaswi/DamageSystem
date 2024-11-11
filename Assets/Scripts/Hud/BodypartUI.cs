using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BodypartUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image image;
    [SerializeField] private IntData maxHealth;

    public UnityEvent<Item> OnConsumableDropped;
    public UnityEvent<int> OnConsumableDroppedWithIndex;
    public UnityEvent<string> OnDisplayHealth;
    private void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();
    }

    public void ColorChange(int health)
    {
        image.color = Color.HSVToRGB(((float)health)/maxHealth.value/4, 0.8f, 0.8f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        var consumable = eventData.pointerDrag.GetComponent<Consumable<HealthSystem>>();
        var dragDrop = eventData.pointerDrag.GetComponent<DragAndDrop>();
        if (consumable && dragDrop)
        {
            OnConsumableDropped?.Invoke(consumable.gameObject.GetComponent<Item>());
            if (consumable as TimedConsumable<HealthSystem> && !consumable.gameObject.activeSelf)
                consumable.transform.SetParent(null, false);
            if(!consumable.gameObject || !consumable.gameObject.activeSelf)
                OnConsumableDroppedWithIndex?.Invoke(dragDrop.Index);
        }

    }

    public void DisplayHealth(int health)
    {
        OnDisplayHealth?.Invoke(health.ToString()+"/"+maxHealth.value.ToString());
    }
}
