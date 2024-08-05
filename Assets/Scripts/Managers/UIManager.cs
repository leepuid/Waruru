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
    [Header("Start UI")]
    [SerializeField] private CanvasGroup startUI;    // Main - startUI
    [SerializeField] private GameObject menu; // startUI 하단 아이콘 묶음
    [SerializeField] private GameObject infoPopUp; // startUI 좌측 상단 아이콘
    [SerializeField] private GameObject info; // Info 버튼
    [SerializeField] private GameObject title; // title
    [SerializeField] private CanvasGroup endUI;   // GameOver UI
    [SerializeField] private GameObject store;    // 상점 UI 버튼
    [SerializeField] private GameObject ad;   // 광고 제거 UI 버튼
    [SerializeField] private GameObject setting;  // 설정 UI 버튼
    [SerializeField] private GameObject touchBlock;

    [Header("InGame UI")]
    [SerializeField] private TMP_Text scoreTxt;   // 실시간 점수 UI 텍스트

    [Header("End UI")]
    [SerializeField] private GameObject share;    // 공유 UI 버튼
    [SerializeField] private TMP_Text bestScoreTxt; // 기록 중 가장 높은 점수
    [SerializeField] private TMP_Text cntScoreTxt;  // 이번 시도의 점수
    [SerializeField] TMP_Text moneyTxt; // 재화 텍스트

    [Header("Ads")]
    [SerializeField] private AdmobManager _admobManager;

    private Stack<GameObject> stack = new();

    private bool isStateCheck = false;
    private bool isPopUpOpen = false;

    private int best;
    public static int money;

    private void Start()
    {
        Opening();

        if (PlayerPrefs.HasKey("BestScore"))
        {
            best = PlayerPrefs.GetInt("BestScore", 0);
            Debug.Log("불러오기");
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", best);
            Debug.Log("저장");
        }

        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", money);
        }
        else
        {
            money = int.Parse(Crypto.LoadEncryptedData("Money"));
        }

        moneyTxt.text = Crypto.LoadEncryptedData("Money");
        bestScoreTxt.text = "Best : " + best.ToString();

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            GameStart();
        }
    }

    private void Update()
    {
        if (Main.Game._gameState == GameState.Over)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Main.Game._gameState = GameState.End;
            }
        }
        if (Main.Game._gameState == GameState.End && !isStateCheck)
        {
            //_admobManager.ShowFrontAd();
            GameOver();
        }
    }

    public void Opening()
    {
        startUI.gameObject.SetActive(true);
        startUI.DOFade(1, 1.0f).onComplete = () =>
        {
            touchBlock.SetActive(false);
        };
    }

    public void GameStart()
    {
        if (startUI != null && !isPopUpOpen)
        {
            startUI.DOFade(0, 1.0f).OnComplete(() =>
            {
                startUI.gameObject.SetActive(false);
                Main.Game._gameState = GameState.Play;
            });
        }
    }

    public void GameOver()
    {
        // 게임 오버 시, 종료 UI 호출
        if (Main.Game._gameState == GameState.End)
        {
            if (endUI != null)
            {
                endUI.gameObject.SetActive(true);
                isStateCheck = true;
                endUI.DOFade(1, 1.0f);

                SaveScore();
            }
        }
    }

    private void OpenPopUp(GameObject go)
    {
        if (stack.Count > 0)
        {
            GameObject current = stack.Peek();
            ClosePopUp(current);
        }
        go.SetActive(true);
        isPopUpOpen = true;
        info.SetActive(false);
        title.SetActive(false);
        scoreTxt.enabled = false;
        stack.Push(go);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(go.transform.DOScale(1.3f, 0.2f));
        sequence.Append(go.transform.DOScale(1.0f, 0.2f));
        sequence.Play();
    }

    private void ClosePopUp(GameObject go)
    {
        if (stack.Count > 0 && stack.Peek() == go)
        {
            GameObject current = stack.Pop();
            info.SetActive(true);
            title.SetActive(true);
            scoreTxt.enabled = true;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(go.transform.DOScale(1.0f, 0.1f));
            sequence.Append(go.transform.DOScale(0.3f, 0.1f));
            sequence.OnComplete(() => go.SetActive(false));
            isPopUpOpen = false;
            sequence.Play();
        }
    }

    private void TogglePopUp(GameObject go)
    {
        if (isPopUpOpen && stack.Peek() == go)
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
        TogglePopUp(store);
    }

    public void SettingPopUp()
    {
        TogglePopUp(setting);
    }

    public void InfoPopUp()
    {
        TogglePopUp(infoPopUp);
    }

    public void SetScoreText(int score)
    {
        scoreTxt.text = score.ToString();
    }

    public void Restart()
    {
        Main.Game._gameState = GameState.Ready;
        isStateCheck = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SaveScore()
    {
        int cnt = int.Parse(scoreTxt.text);
        PlayGamesPlatform.Instance.ReportScore(cnt, GPGSIds.leaderboard_score, (bool success) => { });
        cntScoreTxt.text = "Score : " + cnt;
        money += cnt;
        Crypto.SaveEncryptedData("Money", money.ToString());
        string moneyData = Crypto.LoadEncryptedData("Money");
        UpdateMoneyText(moneyData);
        if (cnt > best)
        {
            best = cnt;
            PlayerPrefs.SetInt("BestScore", best);
            PlayerPrefs.Save();
            bestScoreTxt.text = "Best : " + best.ToString();
            PlayGamesPlatform.Instance.ReportScore(best, GPGSIds.leaderboard_score, (bool success) => { });
        }
    }

    //public void ShowLeaderBoard()
    //{
    //    PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_score);
    //}

    //public void ShowAchievementUI()
    //{
    //    PlayGamesPlatform.Instance.ShowAchievementsUI();
    //}

    public void UpdateMoneyText(string moneyData)
    {
        moneyTxt.text = moneyData;
    }
}