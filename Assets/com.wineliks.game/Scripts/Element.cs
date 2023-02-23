using UnityEngine;
using UnityEngine.EventSystems;

public class Element : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject element;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Level.Instance.InstElement(element);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Camera.main.ScreenToViewportPoint(eventData.position);
        Level.Instance.UpdateElementPosition(position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Level.Instance.SetElement();
    }
}
