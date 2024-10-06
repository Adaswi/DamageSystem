using UnityEngine;

public class Weapon : MonoBehaviour
{
    public IntData attack;
    public FloatData speed;
    public FloatData range;
    public ListFloatData effects;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Pickupable");   
    }
}
