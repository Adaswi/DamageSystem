using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData data;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Weapon");   
    }

    public abstract void Attack();
}
