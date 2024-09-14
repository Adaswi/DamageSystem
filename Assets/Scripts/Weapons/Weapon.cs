using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData data;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Weapon");   
    }
}
