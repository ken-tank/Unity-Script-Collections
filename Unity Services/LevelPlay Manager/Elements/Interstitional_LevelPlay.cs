using System;
using UnityEngine;
using UnityEngine.Events;

public class Interstitional_LevelPlay : MonoBehaviour
{
    [Serializable]
    public class AvailableEvents {
        public UnityEvent onLoad;
        public UnityEvent onLoadFailed;
        public UnityEvent onShow;
        public UnityEvent onShowFailed;
        public UnityEvent onClick;
        public UnityEvent onOpen;
        public UnityEvent onClose;
    }

    [Header("Properties")]
    [SerializeField] bool showOnEnable;

    [Space(20)]
    public AvailableEvents availableEvents;

    void Start() 
    {
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;        
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent; 
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent; 
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        if (!IronSource.Agent.isInterstitialReady()) Load();
        else
        {
            if (showOnEnable) Show();
        }
    }

    void InterstitialAdLoadFailedEvent (IronSourceError error) 
    {
        availableEvents.onLoadFailed.Invoke();
    }

    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        availableEvents.onShowFailed.Invoke();
        Load();
    }

    void InterstitialAdClickedEvent () 
    {
        availableEvents.onClick.Invoke();
    }

    void InterstitialAdClosedEvent () 
    {
        availableEvents.onClose.Invoke();
    }

    bool once = true;
    void InterstitialAdReadyEvent() 
    {
        availableEvents.onLoad.Invoke();
        if (showOnEnable && once) Show();
    }

    void InterstitialAdOpenedEvent() 
    {
        availableEvents.onOpen.Invoke();
    }
  
    void InterstitialAdShowSucceededEvent() 
    {
        availableEvents.onShow.Invoke();
        once = false;
        Load();
    }

    public void Load() 
    {
        IronSource.Agent.loadInterstitial();
    }

    public void Show() 
    {
        IronSource.Agent.showInterstitial();
    }
}
