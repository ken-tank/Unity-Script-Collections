using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    public UnityEvent onDialog, onFinish;
    [SerializeField] Image icon;
    [SerializeField] Text nama, content;
    [SerializeField] GameObject finishLine;
    [SerializeField] List<DialogContent> dialogs;
    Queue<DialogContent> queue;

    void OnEnable()
    {
        queue = new Queue<DialogContent>();
        foreach (var item in dialogs)
        {
            queue.Enqueue(item);
        }

        StartCoroutine(StartDialogs());
    }

    IEnumerator PlayDialog(DialogContent dialog)
    {
        dialog.onThisDialog.Invoke();
        icon.sprite = dialog.icon;
        nama.text = dialog.name;
        Queue<string> hruf = new Queue<string>();
        string kalimat = "";
        foreach(var item in dialog.dialog)
        {
            hruf.Enqueue(item.ToString());
        }

        yield return null;

        while (hruf.Count > 0)
        {
            kalimat += hruf.Dequeue();
            content.text = kalimat;
            if (Input.GetMouseButtonDown(0))
            {
                content.text = dialog.dialog;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(dialog.delay);
    }
    
    IEnumerator StartDialogs()
    {
        onDialog.Invoke();

        while (queue.Count > 0)
        {
            finishLine.SetActive(false);
            DialogContent item = queue.Dequeue();

            yield return StartCoroutine(PlayDialog(item));

            finishLine.SetActive(true);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        yield return null;
        onFinish.Invoke();
    }
}

[System.Serializable]
class DialogContent
{
    public Sprite icon;
    public string name;
    public float delay = 1;
    [TextArea(15, 15)] public string dialog;
    public UnityEvent onThisDialog;
}
