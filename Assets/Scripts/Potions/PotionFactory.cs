using UnityEngine;

public abstract class PotionFactory : MonoBehaviour
{
    public Transform potionContainer;
    public abstract void CreatePotion(Vector3 position);
}
