using System.Collections.Generic;
using UnityEngine;

public class Bodypart : MonoBehaviour
{
    [SerializeField] public BodypartData data;
    [SerializeField] protected HealthSystem healthSystem;
    [SerializeField] protected Entity entity;

    public GameEvent<HitData> OnBodypartHit;

    public int Health
    {
        get => healthSystem.Health;
        set => healthSystem.Health = value;
    }

    private void Awake()
    {
        if(healthSystem == null)
        healthSystem = GetComponent<HealthSystem>();
        if(entity == null)
        entity = GetComponentInParent<Entity>();
    }

    private void Start()
    {
        if (healthSystem != null && Health == 0)
        {
            healthSystem.FullyHeal();
        }
    }

    public void Hit(HitData hitData)
    {
        if(hitData.HitObject.GetComponent<Bodypart>() == this && entity.Health != 0)
        {
            if (healthSystem != null)
            {
                OnHit();
                healthSystem.DealDamage(hitData.weapon.data.attack, hitData.weapon.data.effects);

                Debug.Log("Bodypart " + gameObject.name + " hit! Health left: " + Health);
            }

            OnBodypartHit.Raise(new HitData(this.gameObject, hitData.AttackerObject, hitData.weapon));
        }
    }

    public void Heal()
    {
        if (healthSystem != null)
        {

        }
    }

    public void Death()
    {
        OnDeath();
        data.damageMultiplier = data.afterDeathMultiplier;
        Debug.Log("Bodypart " + gameObject.name + " died");
    }

    protected virtual void OnHit() { }
    protected virtual void OnDeath() { }
}
