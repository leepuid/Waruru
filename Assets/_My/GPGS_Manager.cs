using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSManager : MonoBehaviour
{
    public void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }
    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            Debug.Log($"유저 이름 :{PlayGamesPlatform.Instance.GetUserDisplayName()}");
            Debug.Log($"유저 ID :{PlayGamesPlatform.Instance.GetUserId()}");
            Debug.Log("로그인 성공");
        }
        else
        {
            Debug.Log("로그인 실패");
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
}
