using UnityEngine;
using UnityEngine.Events;

public class OnButtonInput : MonoBehaviour
{
    [SerializeField] string buttonName;
    public UnityEvent onDown, onUp, onPress;
    
    void Update()
    {
        if (Input.GetButtonDown(buttonName))
        {
            onDown.Invoke();
        }

        if (Input.GetButtonUp(buttonName))
        {
            onUp.Invoke();
        }

        if (Input.GetButton(buttonName))
        {
            onPress.Invoke();
        }
    }
}
