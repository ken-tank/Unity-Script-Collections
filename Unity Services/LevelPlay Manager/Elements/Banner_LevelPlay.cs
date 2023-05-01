using System;
using UnityEngine;
using UnityEngine.Events;

public class Banner_LevelPlay : MonoBehaviour
{
    [Serializable]
    public class AvailableEvents {
        public UnityEvent onLoad;
        public UnityEvent onLoadFailed;
        public UnityEvent onClick;
        public UnityEvent onScreenPresented;
        public UnityEvent onScreenDesmissed;
        public UnityEvent onLeftApplication;
    }

    enum BannerSize {
            BANNER, LARGE, RECTANGLE, SMART
    }

    [Header("Properties")]
    [SerializeField] bool showOnEnable = true;
    [SerializeField] BannerSize _bannerSize;
    IronSourceBannerSize bannerSize;
    [SerializeField] IronSourceBannerPosition bannerPosition = IronSourceBannerPosition.TOP;

    [Space(20)]
    public AvailableEvents availableEvents;

    void Awake() 
    {
        Intialize();
    }

    void Intialize() 
    {
        switch (_bannerSize)
        {
            case BannerSize.BANNER:
            bannerSize = IronSourceBannerSize.BANNER;
            break;

            case BannerSize.LARGE:
            bannerSize = IronSourceBannerSize.LARGE;
            break;

            case BannerSize.RECTANGLE:
            bannerSize = IronSourceBannerSize.RECTANGLE;
            break;

            case BannerSize.SMART:
            bannerSize = IronSourceBannerSize.SMART;
            break;
        }
    }

    void OnEnable()
    {
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;

        Load();
    }

    void OnDisable()
    {
        Hide();
    }

    void OnDestroy()
    {
        this.Destroy();
    }

    private void BannerOnAdLeftApplicationEvent(IronSourceAdInfo obj)
    {
        availableEvents.onLeftApplication.Invoke();
    }

    private void BannerOnAdScreenDismissedEvent(IronSourceAdInfo obj)
    {
        availableEvents.onScreenDesmissed.Invoke();
    }

    private void BannerOnAdScreenPresentedEvent(IronSourceAdInfo obj)
    {
        availableEvents.onScreenPresented.Invoke();
    }

    private void BannerOnAdClickedEvent(IronSourceAdInfo obj)
    {
        availableEvents.onClick.Invoke();
        Load();
    }

    private void BannerOnAdLoadFailedEvent(IronSourceError obj)
    {
        availableEvents.onLoadFailed.Invoke();
    }

    private void BannerOnAdLoadedEvent(IronSourceAdInfo obj)
    {
        availableEvents.onLoad.Invoke();
        if (showOnEnable && once) 
        {
            Show();
            once = false;
        }
    }

    bool once = true;
    public void Load() 
    {
        try {
            IronSource.Agent.loadBanner(bannerSize, bannerPosition);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void Show() 
    {
        IronSource.Agent.displayBanner();
    }

    public void Hide() 
    {
        IronSource.Agent.hideBanner();
    }

    public void Destroy() 
    {
        IronSource.Agent.destroyBanner();
    }
}
