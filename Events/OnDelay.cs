using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnDelay : MonoBehaviour
{
    [SerializeField] float delay;

    [Space(20)]
    public UnityEvent isDone;

    IEnumerator Start() 
    {
        yield return new WaitForSecondsRealtime(delay);
        isDone.Invoke();
    }
}
