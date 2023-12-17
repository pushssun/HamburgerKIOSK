using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ScrollButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isDown = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
    }
}
