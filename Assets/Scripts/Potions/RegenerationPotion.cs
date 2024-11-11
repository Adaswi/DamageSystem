using UnityEngine;

public class RegenerationPotion : TimedConsumable<HealthSystem>
{
    [SerializeField] private IntData healAmount;
    private float tickLenght;
    public int VariantID;

    public override void Consume()
    {
        tickLenght = duration.value / (float) healAmount.value;
        component.Heal(1);
        InvokeRepeating(nameof(Heal), tickLenght, tickLenght);     
        base.Consume();
    }

    public override void RemoveEffect()
    {
        this.CancelInvoke();
        base.RemoveEffect();
    }

    private void Heal()
    {
        component.Heal(1);
    }
}
