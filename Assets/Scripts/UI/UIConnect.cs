using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIConnect : MonoBehaviour
{
    [Header("Start UI")]
    public CanvasGroup startUI;    // Main - startUI
    public GameObject menu; // startUI 하단 아이콘 묶음
    public GameObject infoPopUp; // startUI 좌측 상단 아이콘
    public GameObject info; // Info 버튼
    public GameObject title; // title
    public CanvasGroup endUI;   // GameOver UI
    public GameObject store;    // 상점 UI 버튼
    public GameObject ad;   // 광고 제거 UI 버튼
    public GameObject setting;  // 설정 UI 버튼
    public GameObject touchBlock;

    [Header("InGame UI")]
    public TMP_Text scoreTxt;   // 실시간 점수 UI 텍스트

    [Header("End UI")]
    public GameObject share;    // 공유 UI 버튼
    public TMP_Text bestScoreTxt; // 기록 중 가장 높은 점수
    public TMP_Text cntScoreTxt;  // 이번 시도의 점수
    public TMP_Text moneyTxt; // 재화 텍스트

    [Header("Ads")]
    public AdmobManager _admobManager;

    public Stack<GameObject> stack = new();

    public bool isStateCheck = false;
    public bool isPopUpOpen = false;

    public int best;
    public static int money;
}
