using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAnimationEvent : MonoBehaviour
{
    [SerializeField] List<StringEvent> events;
    
    public void Event(string value)
    {
        foreach (var item in events)
        {
            if (item._string == value) item.onTrigger.Invoke();
        }
    }
}

[System.Serializable]
class StringEvent
{
    public string _string;
    public UnityEvent onTrigger;
}
