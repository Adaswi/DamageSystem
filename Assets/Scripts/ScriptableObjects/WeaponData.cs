using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public int attack;
    public float speed;
    public float range;
    public List<float> effects;
}
