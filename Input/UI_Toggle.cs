using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_Toggle : MonoBehaviour
{
    [Header("Toggle Properties")]
    public bool isTrue = true;
    public OptionsParameter optionsParameter;
    [SerializeField] Sprite activeSprite, deactiveSprite;

    [Space(20)]
    [Header("Events")]
    public UnityEvent<bool> onChanged;

    // Variable Sementara
    Image image = null;
    string playerPref => GameManager.GetOptionsParameter(optionsParameter);

    void Awake() 
    {
        image = GetComponent<Image>();

        if (playerPref != "") SetValue(PlayerPrefs.GetInt(playerPref, 1) == 1 ? true : false);
    }

    public void SetValue(bool value)
    {
        isTrue = value;
        image.sprite = isTrue ? activeSprite : deactiveSprite;
        if (playerPref != "") 
        {
            SavePref();
        }
        onChanged.Invoke(isTrue);
    }

    public void Toggle() 
    {
        SetValue(!isTrue);
    }

    void SavePref() 
    {
        PlayerPrefs.SetInt(playerPref, isTrue ? 1 : 0);
    }

    private void OnDrawGizmosSelected() 
    {
        if (image == null) image = GetComponent<Image>();
        image.sprite = isTrue ? activeSprite : deactiveSprite;
    }
}
