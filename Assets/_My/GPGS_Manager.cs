using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

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

    public void ShowAchievementUI()
    {
        // 전체 업적 표시.
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    public void IncrementGPGSAchievement()
    {
        // 단계별 업적 증가.
        // PlayGamesPlatform.Instance.IncrementAchievement(/*GPGSIds.단계별 업적 아이디, 성공 시 증가시킬 단계 값, (bool success) ==> { 성공 시 호출할 이벤트나 함수 }*/);
    }

    public void UnlockingGPGSAchievement()
    {
        // 업적 잠금 해제 및 공개.
        //PlayGamesPlatform.Instance.UnlockAchievement(/*GPGSIds.단계별 잠금해제될 업적 아이디, (bool success) ==> { 성공 시 호출할 이벤트나 함수}*/);
    }
}
