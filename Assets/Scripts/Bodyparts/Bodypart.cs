using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bodypart : MonoBehaviour
{
    [SerializeField] public BodypartData data;

    public UnityEvent<int, List<float>> OnHit;
    public UnityEvent<int, List<float>> OnHitWithMultiplier;
    public UnityEvent OnDeath;

    public void Hit(int damage, List<float> effects)
    {       
        InHit();
        var newEffects = new List<float>(effects);
        newEffects.Add(data.damageMultiplier);

        Debug.Log("Bodypart " + gameObject.name + " hit!");

        OnHit?.Invoke(damage, effects);
        OnHitWithMultiplier?.Invoke(damage, newEffects);
    }

    public void Death()
    {
        InDeath();
        data.damageMultiplier = data.afterDeathMultiplier;

        Debug.Log("Bodypart " + gameObject.name + " died");

        OnDeath?.Invoke();
    }

    protected virtual void InHit() { }
    protected virtual void InDeath() { }
}
