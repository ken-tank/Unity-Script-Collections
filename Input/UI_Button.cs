using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler
{
    [Header("Animation Properties")]
    [SerializeField] float smoothTime = 0.1f;
    [SerializeField] AnimationCurve clickCurve = new AnimationCurve(new Keyframe(0,0), new Keyframe(0.5f,0.2f), new Keyframe(1,0));
    [SerializeField] Vector2 hoverSize = new Vector2(0.05f, 0.05f);

    [Space(20)]
    [Header("Button Properties")]
    [SerializeField] AudioClip clickSound = null;
    [SerializeField] AudioClip hoverSound = null;

    [Space(20)]
    [Header("Events")]
    public UnityEvent onDown;
    public UnityEvent onClick;
    public UnityEvent onUp;
    public UnityEvent<bool> onHover;

    // Variable Sementara
    Vector2 startSize;
    Vector3 velocity = Vector3.zero;
    bool isHover;

    void Awake()
    {
        startSize = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        onHover.Invoke(true);
        isHover = true;
        if (hoverSound != null) GameManager.instance.audioManager.effect.PlayOneShot(hoverSound);
    }
    public void OnPointerExit(PointerEventData data)
    {
        onHover.Invoke(false);
        isHover = false;
    }
    public void OnPointerDown(PointerEventData data)
    {
        Click();
        onDown.Invoke();
        if (clickSound != null) GameManager.instance.audioManager.effect.PlayOneShot(clickSound);
    }
    public void OnPointerClick(PointerEventData data)
    {
        onClick.Invoke();
    }
    public void OnPointerUp(PointerEventData data)
    {
        onUp.Invoke();
    }

    void Update() 
    {
        Hover(isHover);
    }

    void Hover(bool value)
    {
        if (value && transform.localScale.x <= startSize.x + hoverSize.x) transform.localScale += Vector3.SmoothDamp(Vector2.zero, hoverSize, ref velocity, smoothTime, Mathf.Infinity, Time.unscaledDeltaTime);
        else transform.localScale = Vector3.SmoothDamp(transform.localScale, startSize, ref velocity, smoothTime, Mathf.Infinity, Time.unscaledDeltaTime);
    }

    void Click() 
    {
        IEnumerator anim()
        {
            Vector3 clickSize = new Vector2(0.1f, -0.05f);
            float time = 0, t = 0, duration = 0.2f;
            while (time <= duration)
            {
                time += Time.unscaledDeltaTime;
                t = time/duration;

                transform.localScale += clickSize * clickCurve.Evaluate(t);

                yield return null;
            }
        }

        StartCoroutine(anim());
    }
}
