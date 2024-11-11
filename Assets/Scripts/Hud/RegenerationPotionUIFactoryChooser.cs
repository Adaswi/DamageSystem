using UnityEngine;
using UnityEngine.Events;

public class RegenerationPotionUIFactoryChooser : MonoBehaviour
{
    public UnityEvent<int> OnSmallShort;
    public UnityEvent<int> OnSmallLong;
    public UnityEvent<int> OnMediumShort;
    public UnityEvent<int> OnMediumLong;
    public UnityEvent<int> OnBigShort;
    public UnityEvent<int> OnBigLong;
    public UnityEvent<int> OnUnknown;

    public void ChooseFactory(int index, Item item)
    {
        var consumable = item.GetComponent<RegenerationPotion>();
        if (!consumable)
            return;

        switch (consumable.VariantID)
        {
            case 1:
                OnSmallShort?.Invoke(index);
                break;
            case 2:
                OnSmallLong?.Invoke(index);
                break;
            case 3:
                OnMediumShort?.Invoke(index);
                break;
            case 4:
                OnMediumLong?.Invoke(index);
                break;
            case 5:
                OnBigShort?.Invoke(index);
                break;
            case 6:
                OnBigLong?.Invoke(index);
                break;
            default:
                OnUnknown?.Invoke(index);
                break;
        }
    }
}
