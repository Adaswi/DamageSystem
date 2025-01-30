using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bodypart : MonoBehaviour, IBodypart
{
    [SerializeField] private FloatData damageMultiplier;
    [SerializeField] private FloatData afterDeathMultiplier;
    private float currentMultiplier;

    public UnityEvent<int, List<float>> OnHit;
    public UnityEvent<int, List<float>> OnHitWithMultiplier;
    public UnityEvent OnApplyAfterDeathMultiplier;
    public UnityEvent OnApplyStandardMultiplier;

    private void Awake()
    {
        currentMultiplier = damageMultiplier.value;
    }

    public virtual void Hit(int damage, List<float> effects)
    {       
        var newEffects = new List<float>(effects);
        newEffects.Add(currentMultiplier);

        OnHit?.Invoke(damage, effects);
        OnHitWithMultiplier?.Invoke(damage, newEffects);
    }

    public virtual void ApplyStandardMultiplier()
    {
        currentMultiplier = damageMultiplier.value;

        OnApplyStandardMultiplier?.Invoke();
    }

    public virtual void ApplyAfterDeathMultiplier()
    {
        currentMultiplier = afterDeathMultiplier.value;

        OnApplyAfterDeathMultiplier?.Invoke();
    }
}
