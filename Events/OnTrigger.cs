using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] List<string> tags = new List<string>() {"Untagged"};
    public Collider data;
    public UnityEvent<GameObject> onEnter, onExit, onStay;

    void OnTriggerEnter(Collider other)
    {
        Check(other, onEnter);
    }

    void OnTriggerExit(Collider other)
    {
        Check(other, onExit);
    }

    void OnTriggerStay(Collider other)
    {
        Check(other, onStay);
    }

    void Check(Collider other, UnityEvent<GameObject> value)
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
