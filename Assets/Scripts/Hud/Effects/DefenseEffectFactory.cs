using UnityEngine;

public class DefenseEffectFactory : EffectHudFactory
{
    [SerializeField] private DefenseEffectHud effect;

    public override void CreateEffect(float duration)
    {
        var newEffect = Instantiate(effect);
        newEffect.transform.SetParent(transform, false);
        newEffect.Initialize(duration);
    }
}
