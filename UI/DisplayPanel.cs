using UnityEngine;
using UnityEngine.Events;

public class DisplayPanel : MonoBehaviour
{
    public bool display;

    [Space(20)]
    [Header("Available Events")]
    public UnityEvent onDisplay;
    public UnityEvent onUnDisplay;

    public void Display(bool value) 
    {
        display = value;

        if (value)
        {
            onDisplay.Invoke();
        }
        else
        {
            onUnDisplay.Invoke();
        }
    }

    public void Toggle() 
    {
        Display(!display);
    }
}
