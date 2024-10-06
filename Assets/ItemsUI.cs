using UnityEngine;
using UnityEngine.UI;

public class ItemsUI : MonoBehaviour
{
    [SerializeField] private IntData slotsCount;
    [SerializeField] private GameObject itemFrame;
    [SerializeField] private Sprite deafultIcon;
    [SerializeField] private GameObject itemPrefab;

    public GameObject[] ItemFrames;

    private void Awake()
    {
        GenerateSlots();
    }

    public void GenerateSlots()
    {
        ItemFrames = new GameObject[slotsCount.value];
        for (int i = 0; i < slotsCount.value; i++)
        {
            ItemFrames[i] = Object.Instantiate(itemFrame);
            ItemFrames[i].transform.SetParent(itemFrame.transform.parent, false);
        }
        Destroy(itemFrame);
    }

    public GameObject GenerateItem(GameObject item, int index)
    {
        var prefab = Instantiate(itemPrefab);
        var itemComp = prefab.GetComponent<Item>();
        itemComp.FrameIndex = index;

        prefab.transform.SetParent(ItemFrames[index].transform, false);

        var imageComponent = prefab.GetComponentInChildren<Image>();
        imageComponent.sprite = Resources.Load(item.name + "Icon") as Sprite;
        if (imageComponent.sprite == null)
            imageComponent.sprite = deafultIcon;

        return prefab;
    }

    public void RemoveItem(int index)
    {
        foreach(Transform child in ItemFrames[index].transform)
        {
            Destroy(child.gameObject);
        }
    }
}
