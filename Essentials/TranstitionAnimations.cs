using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public enum data {Zoom, Move, Rotate, FadeUI};

public class TranstitionAnimations : MonoBehaviour
{
    [System.Serializable]
    class Transtition 
    {
        public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public data type = data.Zoom;

        public Vector3 newPos;
        [Range(0, 1)] public float fadeValue = 1;
        public UnityEvent onTranstitionFinish;
    }

    [SerializeField] bool playOnAwake = true, unscaledTime = true, loop = false;
    [SerializeField] float duration = 1, delay = 0;
    public bool isLocal = false;
    [SerializeField] List<Transtition> transtitions;

    Vector3 firstPos, firstSize, firstRot;
    float firstAlpha;
    int once = 1, once2 = 1;

    public UnityEvent onFinish;

    IEnumerator _Play(Transtition transtition)
    {
        float time = 0, t = 0;
        data type = transtition.type;
        AnimationCurve curve = transtition.curve;
        Vector3 newPos = transtition.newPos;
        float fadeValue = transtition.fadeValue;
        switch (type)
        {
            case data.Zoom:
                transform.localScale = firstSize * curve.Evaluate(0);
                if (unscaledTime) yield return new WaitForSecondsRealtime(delay);
                else yield return new WaitForSeconds(delay);
                while (time <= duration)
                {
                    if (unscaledTime) time += Time.unscaledDeltaTime;
                    else time += Time.deltaTime;

                    t = Mathf.Clamp01(time/duration);

                    transform.localScale = firstSize * curve.Evaluate(t);

                    yield return null;
                }
                transform.localScale = firstSize * curve.Evaluate(1);
            break;

            case data.Move:
                if(isLocal) transform.localPosition = firstPos + newPos * curve.Evaluate(0);
                else transform.position = firstPos + newPos * curve.Evaluate(0);
                if (unscaledTime) yield return new WaitForSecondsRealtime(delay);
                else yield return new WaitForSeconds(delay);
                while (time <= duration)
                {
                    if (unscaledTime) time += Time.unscaledDeltaTime;
                    else time += Time.deltaTime;

                    t = Mathf.Clamp01(time/duration);

                    if(isLocal) transform.localPosition = firstPos + newPos * curve.Evaluate(t);
                    else transform.position = firstPos + newPos * curve.Evaluate(t);

                    yield return null;
                }
                if(isLocal) transform.localPosition = firstPos + newPos * curve.Evaluate(1);
                else transform.position = firstPos + newPos * curve.Evaluate(1);
            break;

            case data.Rotate:
                if (isLocal) transform.localEulerAngles = firstRot + newPos * curve.Evaluate(0);
                else transform.eulerAngles = firstRot + newPos * curve.Evaluate(0);
                if (unscaledTime) yield return new WaitForSecondsRealtime(delay);
                else yield return new WaitForSeconds(delay);
                while (time <= duration)
                {
                    if (unscaledTime) time += Time.unscaledDeltaTime;
                    else time += Time.deltaTime;

                    t = Mathf.Clamp01(time/duration);

                    if(isLocal) transform.localEulerAngles = firstRot + newPos * curve.Evaluate(t);
                    else transform.eulerAngles = firstRot + newPos * curve.Evaluate(t);

                    yield return null;
                }
                if (isLocal) transform.localEulerAngles = firstRot + newPos * curve.Evaluate(1);
                else transform.eulerAngles = firstRot + newPos * curve.Evaluate(1);
            break;

            case data.FadeUI:
                CanvasGroup canvasGroup = null;
                if (TryGetComponent<CanvasGroup>(out CanvasGroup comp))
                {
                    canvasGroup = comp;
                }
                else
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
                if (once2 == 1) 
                {
                    firstAlpha = canvasGroup.alpha;
                    once2 = 0;
                }
                canvasGroup.alpha = firstAlpha * curve.Evaluate(0);
                if (unscaledTime) yield return new WaitForSecondsRealtime(delay);
                else yield return new WaitForSeconds(delay);
                while (time <= duration)
                {
                    if (unscaledTime) time += Time.unscaledDeltaTime;
                    else time += Time.deltaTime;

                    t = Mathf.Clamp01(time/duration);

                    canvasGroup.alpha = firstAlpha * curve.Evaluate(t);

                    yield return null;
                }
                canvasGroup.alpha = firstAlpha * curve.Evaluate(1);
            break;
        }
        transtition.onTranstitionFinish.Invoke();

        if (loop) StartCoroutine(_Play(transtition));
    }

    void Awake() 
    {
        if (once == 1)
        {
            firstSize = transform.localScale;
            if (isLocal) 
            {
                firstPos = transform.localPosition;
                firstRot = transform.localEulerAngles;
            }
            else 
            {
                firstPos = transform.position;
                firstRot = transform.eulerAngles;
            }
            once = 0;
        }
    }

    void OnEnable() 
    {
        if (playOnAwake) Play();
    }

    public void Play() 
    {
        foreach (var item in transtitions)
        {
            StartCoroutine(_Play(item));
        }
        StartCoroutine(OnEvent());
    }

    public void Play(int transtitionIndex)
    {
        StartCoroutine(_Play(transtitions[Mathf.Clamp(transtitionIndex, 0, transtitions.Count - 1)]));
        StartCoroutine(OnEvent());
    }

    IEnumerator OnEvent()
    {
        if (unscaledTime) yield return new WaitForSecondsRealtime(delay);
        else yield return new WaitForSeconds(delay);
        // Start Animation
        if (unscaledTime) yield return new WaitForSecondsRealtime(duration);
        else yield return new WaitForSeconds(duration);
        // End Animation
        onFinish.Invoke();
    }
}
