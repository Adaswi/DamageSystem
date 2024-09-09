using UnityEngine;

[CreateAssetMenu]
public class BodypartData : ScriptableObject
{
    [Range(1, 9999)]
    [Tooltip("Total health of bodypart")]
    public int maxHp = 100;

    [Range(0f, 10f)]
    [Tooltip("Value that damage to entity is multilpied by")]
    public float damageMultiplier = 1;

    [Range(0f, 10f)]
    [Tooltip("Value that damage to entity is multilpied by after bodypart death")]
    public float afterDeathMultiplier = 2;
}
