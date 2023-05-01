using UnityEngine;
using UnityEngine.Events;

public class OnVisible : MonoBehaviour
{
    public UnityEvent onVisible, onInvisible;

    void Start() 
    {

    }

    private void OnBecameVisible()
    {
        if (this.enabled) onVisible.Invoke();
    }

    private void OnBecameInvisible()
    {
        if (this.enabled) onInvisible.Invoke();
    }
}
