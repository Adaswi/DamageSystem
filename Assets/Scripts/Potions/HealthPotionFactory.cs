using UnityEngine;

public class HealthPotionFactory : PotionFactory
{
    [SerializeField] private HealthPotion healthPotionPrefab;
    public override void CreatePotion(Vector3 position)
    {
        var newHealthPotionObj = Instantiate(healthPotionPrefab.gameObject);
        newHealthPotionObj.transform.SetParent(potionContainer, false);
        newHealthPotionObj.transform.position = position;
    }
}
