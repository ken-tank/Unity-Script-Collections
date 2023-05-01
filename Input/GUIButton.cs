using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GUIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Properties")]
    [SerializeField] Sprite icon;
    [SerializeField] Color iconColor = new Color(0.6f, 0.6f, 0.6f, 1);
    [SerializeField] Image iconImage;
    [Space(10)]
    [SerializeField] Color dimColor = new Color(0.9f, 0.9f, 0.9f, 1);

    [Space(20), Header("Available Events")]
    public UnityEvent onDown;
    public UnityEvent onUp;

    Image image;
    Color startColor;

    private void OnDrawGizmos() 
    {
        if (iconImage)
        {
            if (icon)
            {
                iconImage.sprite = icon;
            }
            iconImage.color = iconColor;
        }
    }

    void Awake() 
    {
        image = GetComponent<Image>();
        startColor = image.color;

        if (iconImage)
        {
            if (icon)
            {
                iconImage.sprite = icon;
            }
            iconImage.color = iconColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onDown.Invoke();
        image.color = startColor * dimColor;
        if (iconImage)
        {
            iconImage.color = iconColor * dimColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onUp.Invoke();
        image.color = startColor;
        if (iconImage)
        {
            iconImage.color = iconColor;
        }
    }
}
