using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationPotion : TimedConsumable<HealthSystem>
{
    [SerializeField] private IntData healAmount;
    private int tickHealth;

    public override void Consume()
    {
        tickHealth = healAmount.value / (int)(duration.value * 10);
        component.Heal(tickHealth);
        InvokeRepeating(nameof(Heal), 0.1f, 0.1f);     
        base.Consume();
    }

    public override void RemoveEffect()
    {
        this.CancelInvoke();
        base.RemoveEffect();
    }

    private void Heal()
    {
        component.Heal(tickHealth);
    }
}
