using UnityEngine;
using UnityEngine.Events;

public class OnHover : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;

    void OnMouseEnter()
    {
        onEnter.Invoke();
    }

    void OnMouseExit()
    {
        onExit.Invoke();
    }
}
