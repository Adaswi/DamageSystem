using UnityEngine;

public struct HitData
{
    public HitData(GameObject HitObject, GameObject AttackerObject, Weapon weapon)
    {
        this.HitObject = HitObject;
        this.AttackerObject = AttackerObject;
        this.weapon = weapon;
    }

    public GameObject HitObject;
    public GameObject AttackerObject;
    public Weapon weapon;
}
