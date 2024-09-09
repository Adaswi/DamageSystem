using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int healthMax;
    [SerializeField] private List<float> internalEffects = new List<float>();

    public Action onDeath;
    public Action OnHealthIncresed;
    public Action OnHealthDecresed;

    public int Health
    {
        get => health;
        set { 
            if (value <= 0)
            {
                health = 0;
                onDeath?.Invoke();
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

    //Deal damage with external effects and internal effects applied
    public int DealDamage(int damage, List<float> externalEffects)
    {
        externalEffects.AddRange(internalEffects);
        foreach (float effect in externalEffects)
        {
            damage = Convert.ToInt32(damage * effect);
        }
        Health -= damage;
        OnHealthDecresed?.Invoke();
        return Health;
    }

    //Deal damage with extrenal effect and internal effects applied
    public int DealDamage(int damage, float externalEffect)
    {
        foreach (float effect in internalEffects)
        {
            damage = Convert.ToInt32(damage * effect);
        }
        damage = Convert.ToInt32(damage * externalEffect);
        Health -= damage;
        OnHealthDecresed?.Invoke();
        return Health;
    }

    //Deal damage with internal effects applied
    public int DealDamage(int damage)
    {
        foreach (float effect in internalEffects)
        {
            damage = Convert.ToInt32(damage*effect);
        }
        Health -= damage;
        OnHealthDecresed?.Invoke();
        return Health;
    }

    //Deal damage with external effects applied and internal effects ignored
    public int DealRawDamage(int damage, List<float> externalEffects)
    {
        foreach (float effect in externalEffects)
        {
            damage = Convert.ToInt32(damage * effect);
        }
        Health -= damage;
        OnHealthDecresed?.Invoke();
        return Health;
    }

    //Deal damage with external effect applied and internal effects ignored
    public int DealRawDamage(int damage, float externalEffect)
    {
        damage = Convert.ToInt32(damage * externalEffect);
        Health -= damage;
        OnHealthDecresed?.Invoke();
        return Health;
    }

    //Deal damage with effects ignored
    public int DealRawDamage(int damage)
    {
        Health -= damage;
        OnHealthDecresed?.Invoke();
        return Health;
    }

    //Restores given amount of health
    public int Heal(int heal)
    {
        Health += heal;
        OnHealthIncresed?.Invoke();
        return Health;
    }

    public int FullyHeal()
    {
        Health = healthMax;
        return Health;
    }

    //Adds an effect and returns its index
    public int AddEffect(float effect)
    {
        internalEffects.Add(effect);
        return internalEffects.Count-1;
    }
    
    //Removes effect at index
    public void RemoveEffect(int effectIndex)
    {
        internalEffects.RemoveAt(effectIndex);
    }

    //Removes all effects
    public void ClearEffects()
    {
        internalEffects.Clear();
    }

    //Brings health to zero
    public void Kill()
    {
        OnHealthDecresed?.Invoke();
        Health = 0;
    }
}
