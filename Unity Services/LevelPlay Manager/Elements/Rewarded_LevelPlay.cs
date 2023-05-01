using System;
using UnityEngine;
using UnityEngine.Events;

public class Rewarded_LevelPlay : MonoBehaviour
{
    [Serializable]
    public class AvailableEvents {
        public UnityEvent onLoad;
        public UnityEvent onLoadFailed;
        public UnityEvent onShow;
        public UnityEvent onShowFailed;
        public UnityEvent onClick;
        public UnityEvent onClose;
        public UnityEvent onRewarded;
    }

    [Header("Properties")]
    [SerializeField] bool showOnEnable;

    [Space(20)]
    public AvailableEvents availableEvents;
    
    void Start() 
    {
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;

        if (!IronSource.Agent.isRewardedVideoAvailable()) Load();
        else
        {
            if (showOnEnable) Show();
        }
    }

    private void RewardedVideoOnAdRewardedEvent(IronSourcePlacement arg1, IronSourceAdInfo arg2)
    {
        availableEvents.onRewarded.Invoke();
    }

    private void RewardedVideoOnAdClosedEvent(IronSourceAdInfo obj)
    {
        availableEvents.onClose.Invoke();
    }

    private void RewardedVideoOnAdClickedEvent(IronSourcePlacement arg1, IronSourceAdInfo arg2)
    {
        availableEvents.onClick.Invoke();
    }

    private void RewardedVideoOnAdShowFailedEvent(IronSourceError arg1, IronSourceAdInfo arg2)
    {
        availableEvents.onShowFailed.Invoke();
        Load();
    }

    private void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo obj)
    {
        availableEvents.onShow.Invoke();
        once = false;
        Load();
    }

    private void RewardedVideoOnAdUnavailable()
    {
        availableEvents.onLoadFailed.Invoke();
    }

    bool once = true;
    private void RewardedVideoOnAdAvailable(IronSourceAdInfo obj)
    {
        availableEvents.onLoad.Invoke();
        if (showOnEnable && once) Show();
    }

    public void Load() 
    {
        IronSource.Agent.loadRewardedVideo();
    }

    public void Show() 
    {
        IronSource.Agent.showRewardedVideo();
    }

}
