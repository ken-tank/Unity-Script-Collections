using UnityEngine;
using UnityEngine.Events;

public class OnHierarchy : MonoBehaviour
{
    public UnityEvent onAwake, onStart, onEnable, onDisable, onDestroy;

    void Awake() 
    {
        onAwake.Invoke();
    }

    void Start() 
    {
        onStart.Invoke();
    }

    void OnEnable() 
    {
        onEnable.Invoke();
    }

    void OnDisable() 
    {
        onDisable.Invoke();
    }

    void OnDestroy() 
    {
        onDestroy.Invoke();
    }

    public void DestroySelf() 
    {
        Destroy(gameObject);
    }

    public void ClearParent() 
    {
        transform.SetParent(null);
    }
}
