using UnityEngine;

public class HealthPotionUIFactory : ItemUIFactory
{
    [SerializeField] private HealthPotionUI healthPotionUI;

    public override void CreateItemUI(int index)
    {
        GameObject newItemUI = Instantiate(healthPotionUI.gameObject);
        newItemUI.transform.SetParent(itemContainer.GetChild(index), false);
        var newHealthPotionUI = newItemUI.GetComponent<HealthPotionUI>();
        newHealthPotionUI.Initialize();
    }
}
