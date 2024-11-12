using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField] private IntData defaultAttack;
    private int currentAttack;
    public int Attack
    {
        get { return currentAttack; }
        set { currentAttack = value; }
    }
    [SerializeField] private FloatData defaultSpeed;
    private float currentSpeed;
    public float Speed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }
    [SerializeField] private FloatData defaultRange;
    private float currentRange;
    public float Range
    {
        get { return currentRange; }
        set { currentRange = value; }
    }
    [SerializeField] private ListFloatData defaultEffects;
    private List<float> currentEffects;
    public List<float> Effects
    {
        get { return currentEffects; }
        set { currentEffects = value; }
    }

    private void Awake()
    {
        Attack = defaultAttack.value;
        Speed = defaultSpeed.value;
        Range = defaultRange.value;
        Effects = defaultEffects.values;
    }
}
