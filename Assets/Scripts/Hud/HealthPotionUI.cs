using UnityEngine;

public class HealthPotionUI : MonoBehaviour, IItemUI
{
    public void Initialize()
    {
    }
    public void Discrad()
    {
        Destroy(gameObject);
    }
}
