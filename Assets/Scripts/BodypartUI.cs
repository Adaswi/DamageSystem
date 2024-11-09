using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BodypartUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image image;
    [SerializeField] private IntData maxHealth;

    public UnityEvent<Item> OnConsumableDropped;
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
        Debug.Log("test");
        var consumable = eventData.pointerDrag.GetComponent<Consumable<HealthSystem>>();
        if (consumable)
        {
            OnConsumableDropped?.Invoke(consumable.gameObject.GetComponent<Item>());
            Destroy(eventData.pointerDrag);
        }
    }

    public void DisplayHealth(int health)
    {
        OnDisplayHealth?.Invoke(health.ToString()+"/"+maxHealth.value.ToString());
    }
}
