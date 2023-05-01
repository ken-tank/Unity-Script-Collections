using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelPlay_Manager : MonoBehaviour
{
    [Serializable]
    public class AvailableEvents {
        public UnityEvent onIntializationCompleate;
        public UnityEvent onIntializationFailed;
    }

    [Header("Properties")]
    public static bool isIntialized;

    [Space(20)]
    public AvailableEvents availableEvents;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Intialize();
    }

    public void Intialize() 
    {
        string AndroidID = Resources.Load<IronSourceMediationSettings>("IronSourceMediationSettings").AndroidAppKey;
        string IosID = Resources.Load<IronSourceMediationSettings>("IronSourceMediationSettings").IOSAppKey;
        string appKey = GetPlatformKey(AndroidID, IosID);
        try {
            IronSource.Agent.init(appKey);
            IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
            isIntialized = true;
            Debug.Log("Mediation Intialized");
        }
        catch (System.Exception e) 
        {
            Debug.LogException(e);
            Debug.Log("Failed To Intialized");
            availableEvents.onIntializationFailed.Invoke();
        }
    }

    public static string GetPlatformKey(string android, string ios) 
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return android;
        }
        else
        {
            return ios;
        }
    }

    void SdkInitializationCompletedEvent() {
        availableEvents.onIntializationCompleate.Invoke();
    }
}
