using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//sealed 키워드 상속 불가
public sealed class Main : Singleton<Main>
{
    Main() { }

    #region Fields
    private readonly GameManager _gameManager = new();
    private readonly DataManager _dataManager = new();
    private readonly PoolManager _poolManager = new();
    private readonly ResourceManager _resourceManager = new();
    //private readonly ScenesManager _scenesManager = new();
    //private readonly UIManager _uiManager = new();
    //private SoundManager _soundManager = new();
    #endregion

    #region Properties
    public static GameManager Game => ins._gameManager;
    public static DataManager Data => ins._dataManager;
    public static PoolManager Pool => ins._poolManager;
    public static ResourceManager Resource => ins._resourceManager;
    //public static ScenesManager Scenes => ins._scenesManager;
    //public static UIManager UI => ins._uiManager;
    #endregion

    #region ExitGame
    private int backBtnCount = 0;
    private float time;
    private float inTime = 1.5f;

    private void Update()
    {
        // 게임 종료.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.time - time < inTime) backBtnCount++;
            else backBtnCount = 1;

            time = Time.time;
            if (backBtnCount >= 2) ExitGame();
            else Debug.Log("Press again to exit the application.");
        }
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
    #endregion
}
