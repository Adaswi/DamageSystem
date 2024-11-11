using UnityEngine;

public class DefensePotionUIFactory : ItemUIFactory
{
    [SerializeField] private DefensePotionUI defensePotionUI;

    public override void CreateItemUI(int index)
    {
        GameObject newItemUI = Instantiate(defensePotionUI.gameObject);
        newItemUI.transform.SetParent(itemContainer.GetChild(index), false);
        var newHealthPotionUI = newItemUI.GetComponent<DefensePotionUI>();
        newHealthPotionUI.Initialize();
    }
}
