using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField] private IntData attack;
    public int Attack
    {
        get => attack.value;
    }
    [SerializeField] private FloatData speed;
    public float Speed
    {
        get => speed.value;
    }
    [SerializeField] private FloatData range;
    public float Range
    {
        get => range.value;
    }
    [SerializeField] private ListFloatData effects;
    public List<float> Effects
    {
        get => effects.values;
    }

    public bool IsPiercing;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Pickupable");   
    }
}
