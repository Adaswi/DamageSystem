using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private IntData healthMax;
    [SerializeField] private int health;
    [SerializeField] private List<float> internalEffects = new List<float>();

    public UnityEvent<int> OnDealDamage;
    public UnityEvent<int> OnHeal;
    public UnityEvent<int> OnHealthUpdate;
    public UnityEvent OnDeath;
    public UnityEvent OnHealthy;

    public int Health
    {
        get => health;
        set { 
            if (value <= 0 && health != 0) //When health reaches zero for the first time
            {
                health = 0;
                OnDeath?.Invoke();
            }
            else if (value <= 0) //When health reaches zero even if it's already zero
            {
                health = 0;
            }
            else if (value >= HealthMax && health != HealthMax)
            {
                health = HealthMax;
                OnHealthy?.Invoke();
            }
            else if (value >= HealthMax)
            {
                health = HealthMax;
            }
            else
            {
                health = value;
            }
            OnHealthUpdate?.Invoke(health);
        }
    }
    public int HealthMax
    {
        get => healthMax.value;
        private set { healthMax.value = value; }
    }

    private void Awake()
    {
        FullyHeal();
    }

    //Deal damage with external effects and internal effects applied
    public void DealDamage(int damage, List<float> externalEffects)
    {
        var newExternalEffects = new List<float>(externalEffects);
        newExternalEffects.AddRange(internalEffects);
        foreach (float effect in newExternalEffects)
        {
            damage = (int)Math.Round(damage * effect);
        }
        Health -= damage;
        OnDealDamage?.Invoke(damage);
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
        OnDealDamage?.Invoke(damage);
    }

    //Deal damage with internal effects applied
    public void DealDamage(int damage)
    {
        foreach (float effect in internalEffects)
        {
            damage = Convert.ToInt32(damage*effect);
        }
        Health -= damage;
        OnDealDamage?.Invoke(damage);
    }

    //Deal damage with external effects applied and internal effects ignored
    public void DealRawDamage(int damage, List<float> externalEffects)
    {
        foreach (float effect in externalEffects)
        {
            damage = Convert.ToInt32(damage * effect);
        }
        Health -= damage;
        OnDealDamage?.Invoke(damage);
    }

    //Deal damage with external effect applied and internal effects ignored
    public void DealRawDamage(int damage, float externalEffect)
    {
        damage = Convert.ToInt32(damage * externalEffect);
        Health -= damage;
        OnDealDamage?.Invoke(damage);
    }

    //Deal damage with effects ignored
    public void DealRawDamage(int damage)
    {
        Health -= damage;
        OnDealDamage?.Invoke(damage);
    }

    //Restores given amount of health
    public void Heal(int heal)
    {
        Health += heal;
        OnHeal?.Invoke(heal);   
    }

    //Restores the percentage of max health
    public void HealByPercentage(float heal)
    {
        heal = Mathf.Clamp(heal, 0, 100);
        var healValue = Convert.ToInt32(HealthMax * heal);
        Health += healValue;
        OnHeal?.Invoke(healValue);
    }

    public void FullyHeal()
    {
        Health = HealthMax;
        OnHeal?.Invoke(HealthMax);
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
        Health = 0;
        OnDealDamage?.Invoke(HealthMax);
    }
}
