using UnityEngine;
using Unity.Services.Authentication;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Threading.Tasks;

public class GooglePlayAuth : MonoBehaviour
{
    [Header("Required AutenticationsManager")]
    public string Token;
    public string Error;

    void Awake() 
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            PlayGamesPlatform.Activate();
            Login();
        }
    }

    public void Login() 
    {
        PlayGamesPlatform.Instance.Authenticate((success) => {
            if (success == SignInStatus.Success)
            {
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, async code => {
                    Token = code;
                    await SignInWithGooglePlayGamesAsync(code);
                });
            }
            else
            {
                Error = "Failed to retrieve Google play games authorization code";
            }
        });
    }

    async Task SignInWithGooglePlayGamesAsync(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }
}
