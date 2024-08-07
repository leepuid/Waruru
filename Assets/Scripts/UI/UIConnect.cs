using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIConnect : MonoBehaviour
{
    [Header("Start UI")]
    public CanvasGroup startUI;    // Main - startUI
    public GameObject menu; // startUI �ϴ� ������ ����
    public GameObject infoPopUp; // startUI ���� ��� ������
    public GameObject info; // Info ��ư
    public GameObject title; // title
    public CanvasGroup endUI;   // GameOver UI
    public GameObject store;    // ���� UI ��ư
    public GameObject ad;   // ���� ���� UI ��ư
    public GameObject setting;  // ���� UI ��ư
    public GameObject touchBlock;

    [Header("InGame UI")]
    public TMP_Text scoreTxt;   // �ǽð� ���� UI �ؽ�Ʈ

    [Header("End UI")]
    public GameObject share;    // ���� UI ��ư
    public TMP_Text bestScoreTxt; // ��� �� ���� ���� ����
    public TMP_Text cntScoreTxt;  // �̹� �õ��� ����
    public TMP_Text moneyTxt; // ��ȭ �ؽ�Ʈ

    [Header("Ads")]
    public AdmobManager _admobManager;

    public Stack<GameObject> stack = new();

    public bool isStateCheck = false;
    public bool isPopUpOpen = false;

    public int best;
    public static int money;
}
