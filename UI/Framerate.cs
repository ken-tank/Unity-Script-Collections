using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Framerate : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] float updateInterval = 1;

    int fps => (int) (1/Time.unscaledDeltaTime);

    void Awake() 
    {
        StartCoroutine(play());
    }
    
    IEnumerator play() 
    {
        while (true)
        {
            if (text.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI tm))
            {
                tm.text = fps.ToString();
            }
            if (text.TryGetComponent<Text>(out Text te))
            {
                te.text = fps.ToString();
            }
            yield return new WaitForSecondsRealtime(updateInterval);
        }
    }
}
