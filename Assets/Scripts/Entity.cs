using System.Collections.Generic;
using UnityEngine;

public class Entity : EntitySystem
{

    private void OnEnable()
    {
        entityID.events.OnBodypartHit += Hit;
        entityID.events.OnDeath += Death;
    }

    private void OnDisable()
    {
        entityID.events.OnBodypartHit -= Hit;
        entityID.events.OnDeath -= Death;
    }

    public virtual void Hit(int damage, List<float> effects)
    {
        Debug.Log("Entity " + gameObject.name + " hit!");
        entityID.events.OnEntityHit?.Invoke();
    }

    public virtual void Death()
    {
        Debug.Log("Entity " + gameObject.name + " died");

        entityID.events.OnEntityDeath?.Invoke();
    }
}
