using UnityEngine;

public class DefensePotion : TimedConsumable<HealthSystem>
{
    [SerializeField] private FloatData defenseMultiplier;
    public int VariantID;
    public override void Consume()
    {
        component.AddEffect(defenseMultiplier.value);
        Debug.Log("Defense potion consumed");
        base.Consume();
    }

    public override void RemoveEffect()
    {
        component.RemoveEffect(defenseMultiplier.value);
        base.RemoveEffect();
    }
}
