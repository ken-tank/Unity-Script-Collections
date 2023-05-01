using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    void Awake() 
    {
        IntializeAds();
    }

    public void IntializeAds() 
    {
        _gameId = AdsInitializer.GetCurrentPlatform(_androidGameId, _iOSGameId);
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string msg) 
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {msg}");
    }

    public static string GetCurrentPlatform(string android, string ios)
    {
        return (Application.platform == RuntimePlatform.IPhonePlayer) ? ios : android;
    }
}
