using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class UI_Visible : MonoBehaviour
{
    [Header("Animation Properties")]
    [SerializeField] AnimationCurve sizeAnimation = new AnimationCurve(new Keyframe(0,1.3f), new Keyframe(1,1));
    [Header("Postition Animation")]
    [SerializeField] Vector2 offsetAnimation;
    [SerializeField] AnimationCurve posAnimation = new AnimationCurve(new Keyframe(0,0), new Keyframe(1,1));
    [SerializeField] float duration = 0.3f;

    [Space(20)] 
    [Header("Visible Properties")]
    [SerializeField] bool visibleOnAwake;

    //Variable Sementara
    Vector2 startSize;
    Vector2 startPos;
    Coroutine running = null;
    CanvasGroup canvasGroup;

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y) + offsetAnimation);
    }

    void Awake() 
    {
        startSize = transform.localScale;
        startPos = transform.localPosition;
        canvasGroup = GetComponent<CanvasGroup>();

        if (!visibleOnAwake)
        {
            transform.localScale = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    public async void Visible(bool value)
    {
        if (value) Show();
        else Hide();

        await Task.Yield();
    }

    public async void Show()
    {
        gameObject.SetActive(true);
        IEnumerator anim() 
        {
            Vector2 start = Vector2.zero;
            float time = 0, t = 0;
            while (time <= duration)
            {
                time += Time.unscaledDeltaTime;
                t = time/duration;

                transform.localScale = Vector2.LerpUnclamped(start, startSize, sizeAnimation.Evaluate(t));
                transform.localPosition = startPos + (offsetAnimation * posAnimation.Evaluate(1-t));
                canvasGroup.alpha = Mathf.Lerp(0, 1, t);

                yield return null;
            }
            transform.localScale = startSize;
            canvasGroup.alpha = 1;
        }
        
        if (running != null)
        {
            StopCoroutine(running);
            running = StartCoroutine(anim());
        }
        else
        {
            running = StartCoroutine(anim());
        }

        await Task.Yield();
    }

    public async void Hide() 
    {
        IEnumerator anim() 
        {
            Vector2 start = Vector2.zero;
            float startA = canvasGroup.alpha;
            float time = 0, t = 0;
            while (time <= duration)
            {
                time += Time.unscaledDeltaTime;
                t = time/duration;

                transform.localScale = Vector2.LerpUnclamped(start, startSize, sizeAnimation.Evaluate(1-t));
                transform.localPosition = startPos + (offsetAnimation * posAnimation.Evaluate(t));
                canvasGroup.alpha = Mathf.Lerp(startA, 0, t);

                yield return null;;
            }
            transform.localScale = startSize;
            canvasGroup.alpha = 0;
            gameObject.SetActive(false);
        }

        if (running != null)
        {
            StopCoroutine(running);
            running = StartCoroutine(anim());
        }
        else
        {
            running = StartCoroutine(anim());
        }

        await Task.Yield();
    }
}
