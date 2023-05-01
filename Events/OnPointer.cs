using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEditor;

[AddComponentMenu("Event/On Pointer")]
public class OnPointer : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent<PointerEventData> onClick, onDown, onUp;

    public void OnPointerClick(PointerEventData data)
    {
        onClick.Invoke(data);
    }

    public void OnPointerDown(PointerEventData data)
    {
        onDown.Invoke(data);
    }

    public void OnPointerUp(PointerEventData data)
    {
        onUp.Invoke(data);
    }
}

class Menu : MonoBehaviour
{

}
