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
        //PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_test, 100, (bool success) => { /*성공 시 호출할 이벤트나 함수*/ });
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_test3, 1, (bool success) => { });
    }

    public void GPGSAchievement()
    {
        // 업적 달성.
        PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_test1, 100, (bool success) => { });
    }

    public void UnlockingGPGSAchievement()
    {
        // 업적 잠금 해제 및 공개.
        //PlayGamesPlatform.Instance.UnlockAchievement(/*GPGSIds.단계별 잠금해제될 업적 아이디, (bool success) => { 성공 시 호출할 이벤트나 함수}*/);
        PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_test2, (bool success) => { });
    }
}
