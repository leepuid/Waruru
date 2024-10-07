using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using TMPro;
using System;

public class AdmobManager : MonoBehaviour
{
    public bool isTestMode;

    private void Awake()
    {
        // Google 모바일 광고 SDK 초기화.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // SDK가 초기화되면 호출.
        });
    }

    private void Start()
    {
        LoadBannerAd();
        LoadFrontAd();
    }

    #region 배너 광고
    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    const string bannerID = "ca-app-pub-9932791264329725/8849319856";
    private BannerView _bannerAd;

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // 이전 광고가 있는지 확인 후, 제거 및 해제.
        if (_bannerAd != null)
        {
            DestroyAd();
        }

        // 새 배너 생성.
        _bannerAd = new BannerView(isTestMode ? bannerTestID : bannerID,
            AdSize.Banner, AdPosition.Bottom);
    }

    public void LoadBannerAd()
    {
        // 배너 생성.
        if (_bannerAd == null)
        {
            CreateBannerView();
        }

        // 새 광고 생성.
        var adRequest = new AdRequest();

        // 새 광고 요청.
        Debug.Log("Loading banner ad.");
        _bannerAd.LoadAd(adRequest);
        ListenToAdEvents();
    }

    // 배너광고 로드 시 발생하는 이벤트.
    private void ListenToAdEvents()
    {
        // 광고가 배너에 로드될 때.
        _bannerAd.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerAd.GetResponseInfo());
        };
        // 광고가 배너에 로드되지 않을 때.
        _bannerAd.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
            LoadBannerAd();
        };
        // 광고가 수익을 발생시켰을 때.
        _bannerAd.OnAdPaid += (AdValue adValue) =>
        {
            //수익 금액과 통화 코드를 로그로 출력.
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // 광고에 대한 노출이 기록될 때.
        _bannerAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // 광고가 클릭되었을 때.
        _bannerAd.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // 광고가 열렸을 때.
        _bannerAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // 광고가 닫혔을 때.
        _bannerAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
            LoadBannerAd();
        };
    }

    public void DestroyAd()
    {
        if (_bannerAd != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerAd.Destroy();
            _bannerAd = null;
        }
    }
    #endregion

    #region 전면 광고
    const string frontTestID = "ca-app-pub-3940256099942544/8691691433";
    const string frontID = "ca-app-pub-9932791264329725/7819185505";
    private InterstitialAd _frontAd;

    private void LoadFrontAd()
    {
        // 이전 광고가 있는지 확인 후, 제거 및 해제.
        if (_frontAd != null)
        {
            _frontAd.Destroy();
            _frontAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // 새 광고 생성.
        var adRequest = new AdRequest();

        // 새 광고 요청.
        InterstitialAd.Load(isTestMode ? frontTestID : frontID, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // 전면광고 로드 에러.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _frontAd = ad;
                // 성공한 경우.
                RegisterEventHandlers(_frontAd);
            });
    }

    // 전면광고를 게재할 준비가 되었는지 확인.
    public void ShowFrontAd()
    {
        if (_frontAd != null && _frontAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _frontAd.Show();
        }
        else
        {
            LoadFrontAd(); // 광고 재로드.
            Debug.LogError("Interstitial ad is not ready yet.");

            // 광고가 준비되지 않은 경우 바로 게임 오버 처리 진행.
            UIManager.Instance.GameOver();
        }
    }

    // 전면광고 로드 시 발생하는 이벤트.
    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // 광고가 수익을 발생시켰을 때.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            //수익 금액과 통화 코드를 로그로 출력.
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // 광고에 대한 노출이 기록될 때.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // 광고가 클릭되었을 때.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // 광고가 열렸을 때.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // 광고가 닫혔을 때.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // 광고가 열리는데 실패했을 때.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            // 다시 로드.
            LoadFrontAd();
        };
    }
    #endregion

}
