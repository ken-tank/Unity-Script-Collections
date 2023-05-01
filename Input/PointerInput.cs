using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerInput : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [System.Serializable]
    public class AvailableEvent {
        public UnityEvent<PointerEventData> onClick, onDown, onUp, onEnter, onExit;
    }

    public AvailableEvent availableEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        availableEvent.onClick.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        availableEvent.onDown.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        availableEvent.onUp.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        availableEvent.onEnter.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        availableEvent.onExit.Invoke(eventData);
    }
}
