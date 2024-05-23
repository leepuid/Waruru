using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    private static GameManager _instance;
    private static bool _initialized;

    private State _state = new State();
    private UIManager _uiManager = new UIManager();

    public static State State { get {  return Instance?._state; } }
    public static UIManager _UIManager { get { return Instance?._uiManager; } }

    public static GameManager Instance
    { // 읽기 전용 프로퍼티.
        get
        {
            if (!_initialized)
            { // 초기화가 되지 않았다면 초기화.
                _initialized = true;

                // 오브젝트를 찾아 넣어준다.
                GameObject obj = GameObject.Find("@GameManager");
                if (obj == null)
                { // 오브젝트가 없다면 만든다.
                    obj = new() { name = "@GameManager" };
                    obj.AddComponent<GameManager>();
                    DontDestroyOnLoad(obj); // 씬을 전환해도 파괴되지 않게 한다.
                }
                _instance = obj.GetComponent<GameManager>();
            }

            return _instance;
        }
    }
}
