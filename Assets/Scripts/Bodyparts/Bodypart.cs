using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bodypart : MonoBehaviour, IBodypart
{
    [SerializeField] private FloatData damageMultiplier;
    [SerializeField] private FloatData afterDeathMultiplier;

    public UnityEvent<int, List<float>> OnHit;
    public UnityEvent<int, List<float>> OnHitWithMultiplier;
    public UnityEvent OnDeath;

    public void Hit(int damage, List<float> effects)
    {       
        InHit();
        var newEffects = new List<float>(effects);
        newEffects.Add(damageMultiplier.value);

        Debug.Log("Bodypart " + gameObject.name + " hit!");

        OnHit?.Invoke(damage, effects);
        OnHitWithMultiplier?.Invoke(damage, newEffects);
    }

    public void Death()
    {
        InDeath();
        damageMultiplier.value = afterDeathMultiplier.value;

        Debug.Log("Bodypart " + gameObject.name + " died");

        OnDeath?.Invoke();
    }

    protected virtual void InHit() { }
    protected virtual void InDeath() { }
}
