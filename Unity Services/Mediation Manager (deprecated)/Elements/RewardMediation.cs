using UnityEngine;
using UnityEngine.Events;
using Unity.Services.Mediation;
using System;

public class RewardMediation : MonoBehaviour
{
    [SerializeField] string AndroidID = "Rewarded_Android", IosID = "Rewarded_iOS";
    [SerializeField] bool showOnStart;

    string gameID => MediationManager.GetIDPlatform(AndroidID, IosID);

    [Space(20), Header("Available Events")]
    public UnityEvent onLoad;
    public UnityEvent onLoadFailed;
    public UnityEvent onShow;
    public UnityEvent onShowFailed;
    public UnityEvent onClose;
    public UnityEvent onCompleate;

    IRewardedAd unit;

    void Start()
    {
        Intialize();
        Load();
    }

    public void Intialize() 
    {
        unit = MediationService.Instance.CreateRewardedAd(gameID);
        
        unit.OnLoaded += AdLoaded;
        unit.OnFailedLoad += AdFailedToLoad;
        unit.OnShowed += AdShown;
        unit.OnFailedShow += AdFailedToShow;
        unit.OnClosed += AdClosed;
        unit.OnUserRewarded += UserRewarded;
    }

    public async void Load() 
    {
        try {
            await unit.LoadAsync();
            if (showOnStart) ShowAd();
        } 
        catch (Exception e) {
            Debug.LogException(e);
        }
    }

    public async void ShowAd()
    {
        if (unit.AdState == AdState.Loaded)
        {
            try
            {
                await unit.ShowAsync();
            }
            catch(Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    void AdLoaded(object sender, EventArgs args)
    {
        onLoad.Invoke();
    }

    void AdFailedToLoad(object sender, LoadErrorEventArgs args)
    {
        onLoadFailed.Invoke();
    }

    void AdShown(object sender, EventArgs args)
    {
        onShow.Invoke();
        Load();
    }

    void AdFailedToShow(object sender, ShowErrorEventArgs args)
    {
        onShowFailed.Invoke();
        Load();
    }

    private void AdClosed(object sender, EventArgs e)
    {
        onClose.Invoke();
    }

    void UserRewarded(object sender, RewardEventArgs args)
    {
        onCompleate.Invoke();
    }
}
