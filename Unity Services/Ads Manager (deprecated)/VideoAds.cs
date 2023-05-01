using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

public class VideoAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string unitId_Android = "Rewarded_Android";
    [SerializeField] string unitId_Ios = "Rewarded_iOS";
    [SerializeField] bool showOnAwake;
    string unitId => AdsInitializer.GetCurrentPlatform(unitId_Android, unitId_Ios);

    [Space(20)]
    [Header("Available Events")]
    public UnityEvent onLoaded;
    public UnityEvent<string> onFailedLoad;
    public UnityEvent onShowStart, onShowClick, onShowCompleate;
    public UnityEvent<string> onShowFailure;

    void Awake() 
    {
        LoadAds();
    }

    void Start() 
    {
        if (showOnAwake) ShowAds();
    }

    void OnEnable()
    {
        if (showOnAwake) ShowAds();
    }

    public void LoadAds() 
    {
        Advertisement.Load(unitId, this);
    }

    public void ShowAds() 
    {
        Advertisement.Show(unitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        onLoaded.Invoke();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        onFailedLoad.Invoke(message);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        onShowClick.Invoke();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        onShowCompleate.Invoke();
        
        if (unitId.Equals(unitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            LoadAds();
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        onShowFailure.Invoke(message);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        onShowStart.Invoke();
        LoadAds();
    }
}
