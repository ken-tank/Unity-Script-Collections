using UnityEngine;
using UnityEngine.Events;
using Unity.Services.Authentication;

public class AutenticationManager : MonoBehaviour
{
    [System.Serializable]
    public class AvailableEvents{
        public UnityEvent onSigned;
        public UnityEvent onSignedFailed;
    }

    public static string playerId;
    public AvailableEvents availableEvents;

    public async void LoginAsGuest()
    {
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        if (AuthenticationService.Instance.IsAuthorized)
        {
            playerId = AuthenticationService.Instance.PlayerId;
            availableEvents.onSigned.Invoke();
        }
        else
        {
            availableEvents.onSignedFailed.Invoke();
        }
    }
}
