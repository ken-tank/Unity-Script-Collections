using UnityEngine;
using UnityEngine.Events;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class UnityServicesManager : MonoBehaviour
{
    enum Environtment {production, tes};

    [System.Serializable] 
    public class AvailableEvents {
        public UnityEvent onInitialized;
        public UnityEvent onIntitializedFailed;
    }

    [SerializeField] Environtment environtment;
    public AvailableEvents availableEvents;

    async void Awake()
    {
        try {
            var options = new InitializationOptions()
                .SetEnvironmentName(environtment == Environtment.production ? "production" : "tes");
            await UnityServices.InitializeAsync(options);
            availableEvents.onInitialized.Invoke();
        } catch {
            availableEvents.onIntitializedFailed.Invoke();
        }
    }
}
