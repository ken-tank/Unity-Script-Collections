using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class OnCollision2D : MonoBehaviour
{
    [SerializeField] List<string> tags = new List<string>() {"Untagged"};
    public Collision2D data;
    public UnityEvent<GameObject> onEnter, onExit, onStay;

    void OnCollisionEnter2D(Collision2D other)
    {
        Check(other, onEnter);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Check(other, onExit);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        Check(other, onStay);
    }

    void Check(Collision2D other, UnityEvent<GameObject> value)
    {
        foreach (string tag in tags)
        {
            if (other.collider.CompareTag(tag))
            {
                data = other;
                value.Invoke(other.collider.gameObject);
            }
        }
    }
}
