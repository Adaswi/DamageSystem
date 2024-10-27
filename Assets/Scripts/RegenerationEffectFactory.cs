using UnityEngine;

public class RegenerationEffectHudFactory : EffectHudFactory
{
    [SerializeField] private RegenerationEffectHud effect;

    public override void CreateEffect(float duration)
    {
        var newEffect = Instantiate(effect);
        newEffect.transform.SetParent(transform, false);
        newEffect.Initialize(duration);
    }
}
