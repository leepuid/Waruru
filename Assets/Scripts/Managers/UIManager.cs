using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header ("Start UI")]
    [SerializeField]  private CanvasGroup startUI;    // Main화면 UI
    [SerializeField]  private CanvasGroup endUI;   // GameOver UI
    [SerializeField] private GameObject store;    // 상점 UI 버튼
    [SerializeField] private GameObject ad;   // 광고 제거 UI 버튼
    [SerializeField] private GameObject setting;  // 설정 UI 버튼
    [SerializeField] private GameObject buyCheck; // 구매 의사를 묻는 UI
    [SerializeField] private GameObject[] items;

    [Header ("InGame UI")]
    [SerializeField] private TMP_Text scoreTxt;   // 실시간 점수 UI 텍스트

    [Header ("End UI")]
    [SerializeField] private GameObject share;    // 공유 UI 버튼
    [SerializeField] private TMP_Text bestScoreTxt; // 기록 중 가장 높은 점수
    [SerializeField] private TMP_Text cntScoreTxt;  // 이번 시도의 점수
    [SerializeField] private TMP_Text moneyTxt; // 재화 텍스트
    
    private Stack<GameObject> stack = new();
    
    private bool isStateCheck = false;
    private bool isPopUpOpen = false;

    private int best;
    private static int money;

    void Start()
    {
        Opennig();
        best = PlayerPrefs.GetInt("BestScore", 0);
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", money);
            Debug.Log("돈이다");
        }
        else
        {
            money = int.Parse(Crypto.LoadEncryptedData("Money"));
        }
        bestScoreTxt.text = "Best : " + best.ToString();
        Debug.Log(best);
        if (Input.GetMouseButton(0))
        {
            GameStart();
        }
    }

    private void Update()
    {
        if(Main.Game._gameState == GameState.End && !isStateCheck)
        {
            GameOver();
        }
    }

    public void Opennig()
    {
        startUI.gameObject.SetActive(true);
        startUI.DOFade(1, 1.0f);
    }

    public void GameStart()
    {
        //if (Input.touchCount > 0)
        //{
        //Touch touch = Input.GetTouch(0);
        //if (touch.phase == TouchPhase.Ended)
        //{

        //panel.SetActive(false);
        //}
        //}
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
                Debug.Log(best);
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
        if(isPopUpOpen && stack.Peek() == go)
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

    public void BuyPopUpOpen()
    {
        buyCheck.SetActive(true);
    }
    public void BuyPopUpClose()
    {
        buyCheck.SetActive(false);
    }

    public void SetScoreText(int score)
    {
        scoreTxt.text = score.ToString();
    }

    public void Restart()
    {
        Main.Game._gameState = GameState.Ready;
        isStateCheck = false;
        SceneManager.LoadScene(0);
    }

    private void SaveScore()
    {
        int cnt = int.Parse(scoreTxt.text);
        cntScoreTxt.text = "Score : " + cnt;
        money += cnt;
        Crypto.SaveEncryptedData("Money", money.ToString());
        string moneyData = Crypto.LoadEncryptedData("Money");
        moneyTxt.text = moneyData;
        if (cnt > best)
        {
            best = cnt;
            PlayerPrefs.SetInt("BestScore", best);
            Debug.Log(best);
            PlayerPrefs.Save();
            bestScoreTxt.text = "Best : " + best.ToString();
        }
    }
}