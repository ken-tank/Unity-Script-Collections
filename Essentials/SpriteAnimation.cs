using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [Tooltip("Animation Interfal In Milisecond")]
    [SerializeField] float interfal;
    [SerializeField] Sprite[] sprites;

    [Space(20)]
    [SerializeField] bool loop;

    [Space(20)]
    public UnityEvent onFinish;

    private void OnDrawGizmosSelected() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else
        {
            spriteRenderer.sprite = null;
        }
    }

    void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator Play() 
    {
        IEnumerator update() 
        {
            foreach (Sprite item in sprites)
            {
                spriteRenderer.sprite = item;

                yield return new WaitForSeconds(interfal/1000);
            }
        }

        if (loop)
        {
            while (true)
            {
                yield return StartCoroutine(update());
            }
        }
        else
        {
            yield return StartCoroutine(update());
            onFinish.Invoke();
        }
    }

    public void DestroySelf() 
    {
        Destroy(gameObject);
    }

    private void OnBecameVisible() 
    {
        if (sprites.Length > 0)
        {
            StartCoroutine(Play());
        }
    }
    private void OnBecameInvisible() 
    {
        if (sprites.Length > 0)
        {
            StopAllCoroutines();
        }
    }
}
