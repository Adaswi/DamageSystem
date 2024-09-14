using System.Collections.Generic;
using UnityEngine;

public class Bodypart : EntitySystem
{
    [SerializeField] private EntityIdInitializer bodypartIdInitializer;
    [SerializeField] private EntityID bodypartID;
    [SerializeField] public BodypartData data;

    protected override void Awake()
    {
        base.Awake();
        if (bodypartIdInitializer == null)
            bodypartIdInitializer = GetComponent<EntityIdInitializer>();
        bodypartID = bodypartIdInitializer.EntityId;
    }

    public void Hit(int damage, List<float> effects)
    {       
        OnHit();
        var newEffects = new List<float>(effects);
        newEffects.Add(data.damageMultiplier);

        Debug.Log("Bodypart " + gameObject.name + " hit!");

        entityID.events.OnHit?.Invoke(damage, newEffects);
        bodypartID.events.OnHit?.Invoke(damage, effects);
    }

    public void Death()
    {
        OnDeath();
        data.damageMultiplier = data.afterDeathMultiplier;

        Debug.Log("Bodypart " + gameObject.name + " died");

        bodypartID.events.OnDeath?.Invoke();
    }

    protected virtual void OnHit() { }
    protected virtual void OnDeath() { }
}
