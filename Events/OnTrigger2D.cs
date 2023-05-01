using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class OnTrigger2D : MonoBehaviour
{
    [SerializeField] List<string> tags = new List<string>() {"Untagged"};
    public Collider2D data;
    public UnityEvent<GameObject> onEnter, onExit, onStay;

    void OnTriggerEnter2D(Collider2D other)
    {
        Check(other, onEnter);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Check(other, onExit);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Check(other, onStay);
    }

    void Check(Collider2D other, UnityEvent<GameObject> value)
    {
        foreach (string tag in tags)
        {
            if (other.CompareTag(tag))
            {
                data = other;
                value.Invoke(other.gameObject);
            }
        }
    }
}
