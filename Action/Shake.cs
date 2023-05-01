using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    Vector3 startPos;

    void Awake() 
    {
        startPos = transform.position;
    }

    public void Shake2D(float power = 0.3f, float waveCount = 5, float duration = 0.1f)
    {
        IEnumerator move(Vector3 offset)
        {
            AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);
            Vector3 pos = transform.position;
            float time = 0;
            float t = 0;
            float dur = duration/waveCount;
            while (time <= dur)
            {
                time += Time.deltaTime;
                t = time/dur;

                transform.position = Vector3.LerpUnclamped(pos, offset, curve.Evaluate(t));

                yield return null;
            }
            transform.position = Vector3.LerpUnclamped(pos, offset, curve.Evaluate(1));
        }
        IEnumerator shake() 
        {
            for (int i = 0; i < waveCount; i++)
            {
                Vector2 circle = Random.insideUnitCircle;
                Vector3 pos = startPos + new Vector3(circle.x, circle.y, 0) * power;
                yield return StartCoroutine(move(pos));
                power = power - (power/waveCount);
            }
            yield return StartCoroutine(move(startPos));
        }

        StopAllCoroutines();
        StartCoroutine(shake());
    }

    public void Shake3D(float power = 0.3f, float waveCount = 5, float duration = 0.1f)
    {
        IEnumerator move(Vector3 offset)
        {
            AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);
            Vector3 pos = transform.position;
            float time = 0;
            float t = 0;
            float dur = duration/waveCount;
            while (time <= dur)
            {
                time += Time.deltaTime;
                t = time/dur;

                transform.position = Vector3.LerpUnclamped(pos, offset, curve.Evaluate(t));

                yield return null;
            }
            transform.position = Vector3.LerpUnclamped(pos, offset, curve.Evaluate(1));
        }
        IEnumerator shake() 
        {
            for (int i = 0; i < waveCount; i++)
            {
                Vector3 pos = startPos + Random.insideUnitSphere * power;
                yield return StartCoroutine(move(pos));
                power = power - (power/waveCount);
            }
            yield return StartCoroutine(move(startPos));
        }

        StopAllCoroutines();
        StartCoroutine(shake());
    }
}
