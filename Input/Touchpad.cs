using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Touchpad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float sensitive = 1;

    [Space(20)]
    [Header("Available Events")]
    public UnityEvent onDown;
    public UnityEvent<Vector2> onDelta;
    public UnityEvent<Vector2> postition;
    public UnityEvent onUp;

    PointerEventData pointer = null;

    public void OnPointerDown(PointerEventData data)
    {
        pointer = data;
        onDown.Invoke();
    }
    public void OnPointerUp(PointerEventData data)
    {
        pointer = null;
        onUp.Invoke();
    }

    void Update() 
    {
        if (pointer != null)
        {
            Vector2 delta = pointer.delta * sensitive * Time.fixedDeltaTime;
            onDelta.Invoke(delta);
            postition.Invoke(pointer.position);
        }
    }

    void OnDisable() 
    {
        pointer = null;
        onUp.Invoke();
    }

    void OnDestroy() 
    {
        pointer = null;
        onUp.Invoke();
    }
}
