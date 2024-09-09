using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Rigidbody[] rigidbodies;

    public GameEvent OnEntityHit;
    public GameEvent OnEntityDeath;

    public int Health
    {
        get => healthSystem.Health;
        set
        {
            healthSystem.Health = value;
            if (Health == 0)
            {
                Death();
            }
        }
    }

    private void Awake()
    {
        if(healthSystem == null)
            healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        if(healthSystem != null && Health == 0)
        {
            healthSystem.FullyHeal();
            healthSystem.onDeath = Death;
        }
    }

    public void Hit(HitData hitData)
    {
        if (this == hitData.HitObject.GetComponentInParent<Entity>())
        if (healthSystem != null)
        {
            var effects = new List<float>(hitData.weapon.data.effects);
            effects.Add(hitData.HitObject.GetComponent<Bodypart>().data.damageMultiplier);
            healthSystem.DealDamage(hitData.weapon.data.attack, effects);

            Debug.Log("Entity " + gameObject.name + " hit! Health left: " + Health);

            OnEntityHit.Raise();
        }
    }

    public void Death()
    {
        Debug.Log("Entity " + gameObject.name + " died");

        OnEntityDeath.Raise();
    }
}
