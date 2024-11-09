using UnityEngine;

public class HealthPotionUI : MonoBehaviour, IItemUI
{
    public int type;

    public void Initialize()
    {
    }
    public void Discrad()
    {
        Destroy(gameObject);
    }
}
