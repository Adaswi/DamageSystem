using UnityEngine;

public class DefensePotionUI : MonoBehaviour, IItemUI
{
    public void Discrad()
    {
        Destroy(gameObject);
    }

    public void Initialize()
    {
        
    }

}
