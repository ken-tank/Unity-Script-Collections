using UnityEngine;
using UnityEngine.Events;
using Unity.Services.Mediation;
using System;

public class BannerMediation : MonoBehaviour
{
    [SerializeField] string AndroidID = "Banner_Android", IosID = "Banner_iOS";
    [SerializeField] BannerAdPredefinedSize _bannerSize;
    BannerAdSize bannerSize => _bannerSize.ToBannerAdSize();
    [SerializeField] BannerAdAnchor bannerAnchor;
    [SerializeField] Vector2 bannerOffset;
    [SerializeField] bool showOnStart = true;

    string gameID => MediationManager.GetIDPlatform(AndroidID, IosID);

    IBannerAd unit;
    int timeOut = 5;

    [Space(20), Header("Available Events")]
    public UnityEvent onLoad;
    public UnityEvent onLoadFailed;
    public UnityEvent onClick;
    public UnityEvent onRefresh;

    void Start()
    {
        Intialize();
    }
    void OnEnable()
    {
        Intialize();
    }
    void OnDisable()
    {
        Hide();
    }
    void OnDestroy()
    {
        Hide();
    }

    public void Intialize() 
    {
        if (MediationManager.isIntialized)
        {
            unit = MediationService.Instance.CreateBannerAd(
                gameID,
                bannerSize, 
                bannerAnchor, 
                bannerOffset
            );

            unit.OnLoaded += AdLoaded;
            unit.OnFailedLoad += AdFailedToLoad;
            unit.OnClicked += AdClick;
            unit.OnRefreshed += AdRefresh;

            if (showOnStart) Load();
        }
        else 
        {
            timeOut -= 1;
            if (timeOut > 0)
            {
                Intialize();
            }
        }
    }

    public async void Load() 
    {
        try {
            await unit.LoadAsync();
        } 
        catch (Exception e) {
            Debug.LogException(e);
        }
    }

    public void Hide() 
    {
        unit.Dispose();
    }

    void AdLoaded(object sender, EventArgs args)
    {
        onLoad.Invoke();
    }

    void AdFailedToLoad(object sender, LoadErrorEventArgs args)
    {
        onLoadFailed.Invoke();
    }

    void AdClick(object sender, EventArgs args)
    {
        onClick.Invoke();
        Load();
    }

    private void AdRefresh(object sender, EventArgs args)
    {
        onRefresh.Invoke();
    }

}
