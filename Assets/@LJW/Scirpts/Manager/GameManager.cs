using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

//sealed 키워드 상속 불가
public sealed class GameManager : Singleton<GameManager>
{
    GameManager() { }

    #region Fields
    private readonly DataManager _dataManager = new();
    private readonly PoolManager _poolManager = new();
    private readonly ResourceManager _resourceManager = new();
    //private readonly ScenesManager _scenesManager = new();
    //private readonly UIManager _uiManager = new();
    //private SoundManager _soundManager = new();
    #endregion

    #region Properties
    public static DataManager Data => ins._dataManager;
    public static PoolManager Pool => ins._poolManager;
    public static ResourceManager Resource => ins._resourceManager;
    //public static ScenesManager Scenes => ins._scenesManager;
    //public static UIManager UI => ins._uiManager;
    #endregion
}
