using UnityEngine;

public class HealthPotion : Consumable<HealthSystem>
{
    [SerializeField] private IntData healAmount;

    public override void Consume()
    {
        component.Heal(healAmount.value);
        base.Consume();
    }
}
