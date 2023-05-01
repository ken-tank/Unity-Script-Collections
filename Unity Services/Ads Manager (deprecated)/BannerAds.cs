using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    [SerializeField] BannerPosition bannerPosition;

    [Space(10)]
    [SerializeField] string unitId_Android = "Banner_Android";
    [SerializeField] string unitId_Ios = "Banner_iOS";
    [SerializeField] bool showOnAwake;
    string unitId => AdsInitializer.GetCurrentPlatform(unitId_Android, unitId_Ios);

    [Space(20)]
    [Header("Available Events")]
    public UnityEvent onBannerLoad;
    public UnityEvent<string> onBannerError;
    public UnityEvent onBannerClick;
    public UnityEvent onBannerHide;
    public UnityEvent onBannerShow;

    void Awake() 
    {
        Advertisement.Banner.SetPosition(bannerPosition);
        LoadBanner();
        if (showOnAwake) ShowBanner();
    }

    void OnEnable() 
    {
        if (showOnAwake) ShowBanner();
    }

    void OnDsable()
    {
        HideBanner();
    }

    void OnDestroy() 
    {
        HideBanner();
    }

    public void SetBannerPostition(BannerPosition bannerPosition)
    {
        this.bannerPosition = bannerPosition;
        Advertisement.Banner.SetPosition(this.bannerPosition);
    }

    public void LoadBanner() 
    {
        BannerLoadOptions options = new BannerLoadOptions {
            loadCallback = OnBannerLoad,
            errorCallback = OnBannerError
        };
        Advertisement.Banner.Load(unitId, options);
    }

    void OnBannerLoad() 
    {
        onBannerLoad.Invoke();
    }

    void OnBannerError(string message) 
    {
        onBannerError.Invoke(message);
    }

    public void ShowBanner() 
    {
        BannerOptions options = new BannerOptions {
            clickCallback = OnBannerClick,
            hideCallback = OnBannerHide,
            showCallback = OnBannerShow
        };

        Advertisement.Banner.Show(AdsInitializer.GetCurrentPlatform(unitId_Android, unitId_Ios), options);
    }

    public void HideBanner() 
    {
        Advertisement.Banner.Hide();
    }

    void OnBannerClick() 
    {
        onBannerClick.Invoke();
    }

    void OnBannerHide() 
    {
        onBannerHide.Invoke();
        LoadBanner();
    }

    void OnBannerShow() 
    {
        onBannerShow.Invoke();
    }
}
