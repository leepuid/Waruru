//using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public GameState _gameState = GameState.Ready;
    public float _timer;
    private int _score;
    private float _currentSpeed = 0.95f;
    private float _currentHue = 0.9f;

    public float GetHue()
    {
        _currentHue += 0.05f;
        if (_currentHue > 1.0f)
            _currentHue = 0.0f;
        return _currentHue;
    }

    public int GetScore() { return _score; }
    public void AddScore()
    {
        _score++;
        UIManager.ins.SetScoreText(_score);
        // 업적.
        //switch (_score)
        //{
        //    case 1:
        //        ReportAchievement(GPGSIds.achievement_first_step);
        //        break;
        //    case 10:
        //        ReportAchievement(GPGSIds.achievement_at_15x_speed);
        //        break;
        //    case 20:
        //        ReportAchievement(GPGSIds.achievement_at_20x_speed);
        //        break;
        //    case 40:
        //        ReportAchievement(GPGSIds.achievement_at_30x_speed);
        //        break;
        //    case 75:
        //        ReportAchievement(GPGSIds.achievement_persistence);
        //        break;
        //    case 100:
        //        ReportAchievement(GPGSIds.achievement_100);
        //        break;
        //    case 500:
        //        PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_god_of_dominoes, (bool success) => { });
        //        break;
        //}
    }

    //private void ReportAchievement(string achievementId)
    //{
    //    PlayGamesPlatform.Instance.ReportProgress(achievementId, 100, (bool success) => { });
    //}

    public void InitScore() { _score = 0; }

    public float GetSpeed()
    {
        if (_currentSpeed <= 2.95f)
        {
            return _currentSpeed += 0.05f;
        }
        else
        {
            // 도미노 40개 및 랜덤 속도 업적 달성.
            //PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_at_random_speed, 100, (bool success) => { });
            return Random.Range(1.0f, 3.1f);
        }
    }
}
public enum GameState
{
    Ready,
    Play,
    Over,
    End
}
