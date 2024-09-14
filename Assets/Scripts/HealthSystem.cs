using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : EntitySystem
{
    [SerializeField] private int health;
    [SerializeField] private int healthMax;
    [SerializeField] private List<float> internalEffects = new List<float>();

    public Action entityIDeventsOnHealthDecreased;

    public int Health
    {
        get => health;
        set { 
            if (value <= 0 && health != 0) //When health reaches zero for the first time
            {
                health = 0;
                entityID.events.OnDeath?.Invoke();
            }
            else if (value <= 0) //When health reaches zero even if it's already zero
            {
                health = 0;
            }
            else if (value > healthMax)
            {
                health = healthMax;
            }
            else
            {
                health = value;
            }
        }
    }

    private void OnEnable()
    {
        entityID.events.OnBodypartHit += DealDamage;
    }

    private void OnDisable()
    {
        entityID.events.OnBodypartHit -= DealDamage;
    }

    //Deal damage with external effects and internal effects applied
    public void DealDamage(int damage, List<float> externalEffects)
    {
        externalEffects.AddRange(internalEffects);
        foreach (float effect in externalEffects)
        {
            damage = (int)Math.Round(damage * effect);
        }
        Health -= damage;
        entityID.events.OnHealthDecreased?.Invoke();
    }

    //Deal damage with extrenal effect and internal effects applied
    public void DealDamage(int damage, float externalEffect)
    {
        foreach (float effect in internalEffects)
        {
            damage = Convert.ToInt32(damage * effect);
        }
        damage = Convert.ToInt32(damage * externalEffect);
        Health -= damage;
        entityID.events.OnHealthDecreased?.Invoke();
    }

    //Deal damage with internal effects applied
    public void DealDamage(int damage)
    {
        foreach (float effect in internalEffects)
        {
            damage = Convert.ToInt32(damage*effect);
        }
        Health -= damage;
        entityID.events.OnHealthDecreased?.Invoke();
    }

    //Deal damage with external effects applied and internal effects ignored
    public void DealRawDamage(int damage, List<float> externalEffects)
    {
        foreach (float effect in externalEffects)
        {
            damage = Convert.ToInt32(damage * effect);
        }
        Health -= damage;
        entityID.events.OnHealthDecreased?.Invoke();
    }

    //Deal damage with external effect applied and internal effects ignored
    public void DealRawDamage(int damage, float externalEffect)
    {
        damage = Convert.ToInt32(damage * externalEffect);
        Health -= damage;
        entityID.events.OnHealthDecreased?.Invoke();
    }

    //Deal damage with effects ignored
    public void DealRawDamage(int damage)
    {
        Health -= damage;
        entityID.events.OnHealthDecreased?.Invoke();
    }

    //Restores given amount of health
    public void Heal(int heal)
    {
        Health += heal;
        entityID.events.OnHealthIncreased?.Invoke();
    }

    public void FullyHeal()
    {
        Health = healthMax;
    }

    //Adds an effect
    public void AddEffect(float effect)
    {
        internalEffects.Add(effect);
    }
    
    //Removes effect
    public void RemoveEffect(float effect)
    {
        internalEffects.Remove(effect);
    }

    //Removes all effects
    public void ClearEffects()
    {
        internalEffects.Clear();
    }

    //Brings health to zero
    public void Kill()
    {
        entityID.events.OnHealthDecreased?.Invoke();
        Health = 0;
    }
}
