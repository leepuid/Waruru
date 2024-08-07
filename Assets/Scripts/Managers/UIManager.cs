using DG.Tweening;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private UIConnect uiCnt;
    private void Start()
    {
        Opening();

        if (PlayerPrefs.HasKey("BestScore"))
        {
            uiCnt.best = PlayerPrefs.GetInt("BestScore", 0);
            Debug.Log("불러오기");
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", uiCnt.best);
            Debug.Log("저장");
        }

        if (!PlayerPrefs.HasKey("Money"))
        {
           // PlayerPrefs.SetInt("Money", money);
        }
        else
        {
            //uiCnt.money = int.Parse(Crypto.LoadEncryptedData(Main.Data.Money.ToString()));
        }

        uiCnt.moneyTxt.text = Crypto.LoadEncryptedData("Money");
        uiCnt.bestScoreTxt.text = "Best : " + uiCnt.best.ToString();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            GameStart();
        }
    }

    private void Update()
    {
        if (Main.Game._gameState == GameState.Over)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Main.Game._gameState = GameState.End;
            }
        }
        if (Main.Game._gameState == GameState.End && !uiCnt.isStateCheck)
        {
            _admobManager.ShowFrontAd();
            GameOver();
        }
    }

    public void Opening()
    {
        uiCnt.startUI.gameObject.SetActive(true);
        uiCnt.startUI.DOFade(1, 1.0f).onComplete = () =>
        {
            uiCnt.touchBlock.SetActive(false);
        };
    }

    public void GameStart()
    {
        if (uiCnt.startUI != null && !uiCnt.isPopUpOpen)
        {
            uiCnt.startUI.DOFade(0, 1.0f).OnComplete(() =>
            {
                uiCnt.startUI.gameObject.SetActive(false);
                Main.Game._gameState = GameState.Play;
            });
        }
    }

    public void GameOver()
    {
        // 게임 오버 시, 종료 UI 호출
        if (Main.Game._gameState == GameState.End)
        {
            if (uiCnt.endUI != null)
            {
                uiCnt.endUI.gameObject.SetActive(true);
                uiCnt.isStateCheck = true;
                uiCnt.endUI.DOFade(1, 1.0f);

                SaveScore();
            }
        }
    }

    private void OpenPopUp(GameObject go)
    {
        if (uiCnt.stack.Count > 0)
        {
            GameObject current = uiCnt.stack.Peek();
            ClosePopUp(current);
        }
        go.SetActive(true);
        uiCnt.isPopUpOpen = true;
        uiCnt.info.SetActive(false);
        uiCnt.title.SetActive(false);
        uiCnt.scoreTxt.enabled = false;
        uiCnt.stack.Push(go);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(go.transform.DOScale(1.3f, 0.2f));
        sequence.Append(go.transform.DOScale(1.0f, 0.2f));
        sequence.Play();
    }

    private void ClosePopUp(GameObject go)
    {
        if (uiCnt.stack.Count > 0 && uiCnt.stack.Peek() == go)
        {
            GameObject current = uiCnt.stack.Pop();
            uiCnt.info.SetActive(true);
            uiCnt.title.SetActive(true);
            uiCnt.scoreTxt.enabled = true;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(go.transform.DOScale(1.0f, 0.1f));
            sequence.Append(go.transform.DOScale(0.3f, 0.1f));
            sequence.OnComplete(() => go.SetActive(false));
            uiCnt.isPopUpOpen = false;
            sequence.Play();
        }
    }

    private void TogglePopUp(GameObject go)
    {
        if (uiCnt.isPopUpOpen && uiCnt.stack.Peek() == go)
        {
            ClosePopUp(go);
        }
        else
        {
            OpenPopUp(go);
        }
    }

    public void StorePopUp()
    {
        TogglePopUp(uiCnt.store);
    }

    public void SettingPopUp()
    {
        TogglePopUp(uiCnt.setting);
    }

    public void InfoPopUp()
    {
        TogglePopUp(uiCnt.infoPopUp);
    }

    public void SetScoreText(int score)
    {
        uiCnt.scoreTxt.text = score.ToString();
    }

    public void Restart()
    {
        Main.Game._gameState = GameState.Ready;
        uiCnt.isStateCheck = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SaveScore()
    {
        int cnt = int.Parse(scoreTxt.text);
        PlayGamesPlatform.Instance.ReportScore(cnt, GPGSIds.leaderboard_score, null);
        cntScoreTxt.text = "Score : " + cnt;
        money += cnt;
        Crypto.SaveEncryptedData("Money", money.ToString());
        string moneyData = Crypto.LoadEncryptedData("Money");
        UpdateMoneyText(moneyData);
        if (cnt > uiCnt.best)
        {
            uiCnt.best = cnt;
            PlayerPrefs.SetInt("BestScore", uiCnt.best);
            PlayerPrefs.Save();
            bestScoreTxt.text = "Best : " + best.ToString();
            PlayGamesPlatform.Instance.ReportScore(best, GPGSIds.leaderboard_score, null);
        }
    }

    public void ShowLeaderBoard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_score);
    }

    public void ShowAchievementUI()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }

    public void UpdateMoneyText(string moneyData)
    {
        uiCnt.moneyTxt.text = moneyData;
    }
}