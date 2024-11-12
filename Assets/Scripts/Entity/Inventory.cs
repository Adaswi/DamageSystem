using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private IntData slotsCount;
    [SerializeField] private GameObject containerObject;
    [SerializeField] private Transform dropperPosition, itemContainer;
    [SerializeField] private float dropSpread = 0.4f;
    private GameObject[] itemFrames;

    public UnityEvent<int, Item> OnStoreItem;

    private void Awake()
    {
        itemFrames = new GameObject[slotsCount.value];
        for (int i = 0; i < slotsCount.value; i++)
        {
            itemFrames[i] = new GameObject("ItemContainer " + i);
            itemFrames[i].transform.SetParent(containerObject.transform, false);
        }
    }

    public void StoreItem(Item item)
    {
        var i = 0;
        foreach (GameObject frame in itemFrames)
        {
            if (frame.transform.childCount > 0)
            {
                i++;
            }
            else
            {
                StoreItemInSlot(i, item);
                OnStoreItem?.Invoke(i, item);
                break;
            }
        }
    }

    public void StoreItemInSlot(int index, Item item)
    {
        item.transform.SetParent(gameObject.transform.GetChild(index), false);
    }

    public void DeleteItem(int index)
    {
        for (int i = 0; i < itemFrames[index].transform.childCount; i++)
        {
            Destroy(itemFrames[index].transform.GetChild(i).gameObject);
        }
    }

    public void DropItem(int index)
    {
        for (int i = 0; i < itemFrames[index].transform.childCount; i++)
        {
            itemFrames[index].transform.GetChild(i).gameObject.transform.position = dropperPosition.position + new Vector3(Random.Range(-dropSpread, dropSpread), Random.Range(-dropSpread, dropSpread), Random.Range(-dropSpread, dropSpread));
            itemFrames[index].transform.GetChild(i).gameObject.transform.rotation = Quaternion.identity;
            itemFrames[index].transform.GetChild(i).gameObject.transform.SetParent(itemContainer);
        }
    }
}
