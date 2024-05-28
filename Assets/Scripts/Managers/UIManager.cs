using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public CanvasGroup startUI;    // Main화면 UI
    public CanvasGroup endUI;   // GameOver UI
    public GameObject store;    // 상점 UI 버튼
    public GameObject share;    // 공유 UI 버튼
    public GameObject ad;   // 광고 제거 UI 버튼
    public GameObject setting;  // 설정 UI 버튼
    public TMP_Text scoreTxt;   // 재화 UI 텍스트
    
    private bool isStateCheck = false;

    private Stack<GameObject> stack = new();

    void Start()
    {
        if (stack.Count == 0)
        {
            if (Input.GetMouseButton(0))    
            {
                GameStart();
            }
        }
    }

    private void Update()
    {
        if(Main.Game._gameState == GameState.End && !isStateCheck)
        {
            GameOver();
        }
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
        if (startUI != null)
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
                endUI.DOFade(1, 1.0f).OnComplete(() =>
                {
                    
                });
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
        stack.Push(go);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(go.transform.DOScale(1.3f, 0.1f));
        sequence.Append(go.transform.DOScale(1.0f, 0.1f));
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
            sequence.Play();
        }
    }

    public void StorePopUp()
    {
        OpenPopUp(store);
    }

    public void SettingPopUp()
    {
        OpenPopUp(setting);
    }

    public void SetScoreText(int score)
    {
        scoreTxt.text = score.ToString();
    }
}
