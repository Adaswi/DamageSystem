using UnityEngine;

public abstract class ItemUIFactory : MonoBehaviour
{
    public Transform itemContainer;
    public abstract void CreateItemUI(int index);
}
