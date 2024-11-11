using UnityEngine;

public class RegenerationPotionUIFactory : ItemUIFactory
{
    [SerializeField] private RegenerationPotionUI regenerationPotionUI;

    public override void CreateItemUI(int index)
    {
        GameObject newItemUI = Instantiate(regenerationPotionUI.gameObject);
        newItemUI.transform.SetParent(itemContainer.GetChild(index), false);
        var newRegenerationPotionUI = newItemUI.GetComponent<RegenerationPotionUI>();
        newRegenerationPotionUI.Initialize();
    }
}
