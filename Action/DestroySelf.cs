using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] bool destroyOnStart;
    [SerializeField] float delay;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        if (destroyOnStart) Destroying();
    }

    public void Destroying()
    {
        Destroy(gameObject);
    }
}
