using UnityEngine;

public class RegenerationPotionUI : MonoBehaviour, IItemUI
{
    public void Initialize()
    {
    }
    public void Discrad()
    {
        Destroy(gameObject);
    }
}
