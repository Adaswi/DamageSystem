using UnityEngine;
using UnityEngine.Events;

public class HealthPotionUIFactoryChooser : MonoBehaviour
{
    public UnityEvent<int> OnSmall;
    public UnityEvent<int> OnMedium;
    public UnityEvent<int> OnBig;
    public UnityEvent<int> OnUnknown;

    public void ChooseFactory(int index, Item item)
    {
        var consumable = item.GetComponent<HealthPotion>();
        if (!consumable)
            return;

        switch (consumable.VariantID)
        {
            case 1:
                OnSmall?.Invoke(index);
                break;
            case 2:
                OnMedium?.Invoke(index);
                break;
            case 3:
                OnBig?.Invoke(index);
                break;
            default:
                OnUnknown?.Invoke(index);
                break;
        }
    }
}
