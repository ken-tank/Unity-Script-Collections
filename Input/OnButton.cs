using UnityEngine;
using UnityEngine.Events;

public class OnButton : MonoBehaviour
{
    [SerializeField] string buttonName;

    [Space(20)]
    public UnityEvent onDown;
    public UnityEvent onHold;
    public UnityEvent onUp;

    private void OnDrawGizmosSelected() 
    {
        try {
            buttonName = buttonName.Trim();
        }
        catch {}
    }

    void Update() 
    {
        if (buttonName != "")
        {
            if (Input.GetButtonDown(buttonName)) onDown.Invoke();
            if (Input.GetButton(buttonName)) onHold.Invoke();
            if (Input.GetButtonUp(buttonName)) onUp.Invoke();
        }
    }
}
