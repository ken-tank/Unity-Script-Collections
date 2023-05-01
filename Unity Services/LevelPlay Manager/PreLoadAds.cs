using UnityEngine;
using System.Threading.Tasks;

public class PreLoadAds : MonoBehaviour
{
    [System.Serializable] 
    class Ads 
    {
        public bool Interstitial;
        public bool Rewarded;
    }

    [SerializeField] Ads preLoadAds;

    float timeout = 10;

    void Start()
    {
        Check();
    }

    async void Check()
    {
        while (timeout >= 0)
        {
            if (LevelPlay_Manager.isIntialized)
            {
                PreLoad();
                Destroy(gameObject);
                break;
            }
            timeout -= Time.unscaledDeltaTime;
            await Task.Yield();
        }
    }

    void LoadInterstitial() 
    {
        if (!IronSource.Agent.isInterstitialReady()) IronSource.Agent.loadInterstitial();
    }

    void LoadRewarded() 
    {
        if (!IronSource.Agent.isRewardedVideoAvailable()) IronSource.Agent.loadRewardedVideo();
    }

    public void PreLoad()
    {
        if (preLoadAds.Interstitial) LoadInterstitial();
        if (preLoadAds.Rewarded) LoadRewarded();
    }
}
