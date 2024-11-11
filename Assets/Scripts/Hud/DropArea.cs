using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public UnityEvent<int> OnDropped;
    public void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<Item>();
        var dragDrop = eventData.pointerDrag.GetComponent<DragAndDrop>();

        if (item && dragDrop)
        {
            OnDropped?.Invoke(dragDrop.Index);
            Destroy(eventData.pointerDrag);
        }
    }
}
