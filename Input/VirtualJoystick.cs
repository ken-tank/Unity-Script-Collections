using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Transform joystick, pad;
    [SerializeField] float padClamp = 50;
    [SerializeField] bool hideOnAwake;

    [Space(10)]
    public Vector2 axis;

    [Space(20)]
    public UnityEvent onDown;
    public UnityEvent<Vector2> onPress;
    public UnityEvent onUp;

    PointerEventData pointer = null;
    Vector2 basePos;

    void Awake() 
    {
        joystick.gameObject.SetActive(!hideOnAwake);
    }

    void Start() 
    {
        basePos = joystick.position;
    }

    void OnDisable() 
    {
        joystick.position = basePos;
        joystick.gameObject.SetActive(!hideOnAwake);
        pad.localPosition = Vector3.zero;
        axis = Vector2.zero;
        onUp.Invoke();
    }

    void OnDestroy() 
    {
        joystick.position = basePos;
        joystick.gameObject.SetActive(!hideOnAwake);
        pad.localPosition = Vector3.zero;
        axis = Vector2.zero;
        onUp.Invoke();
    }

    void Update() 
    {
        if (pointer != null)
        {
            Vector2 local = pointer.position - new Vector2(joystick.position.x, joystick.position.y);

            pad.localPosition = Vector2.ClampMagnitude(local, padClamp);
            axis = pad.localPosition/padClamp;
            onPress.Invoke(axis);
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        pointer = data;
        joystick.position = pointer.position;
        joystick.gameObject.SetActive(true);
        onDown.Invoke();
    }
    public void OnPointerUp(PointerEventData data)
    {
        pointer = null;
        joystick.position = basePos;
        joystick.gameObject.SetActive(!hideOnAwake);
        pad.localPosition = Vector3.zero;
        axis = Vector2.zero;
        onUp.Invoke();
    }
}
