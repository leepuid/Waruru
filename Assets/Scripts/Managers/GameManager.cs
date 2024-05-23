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
    { // �б� ���� ������Ƽ.
        get
        {
            if (!_initialized)
            { // �ʱ�ȭ�� ���� �ʾҴٸ� �ʱ�ȭ.
                _initialized = true;

                // ������Ʈ�� ã�� �־��ش�.
                GameObject obj = GameObject.Find("@GameManager");
                if (obj == null)
                { // ������Ʈ�� ���ٸ� �����.
                    obj = new() { name = "@GameManager" };
                    obj.AddComponent<GameManager>();
                    DontDestroyOnLoad(obj); // ���� ��ȯ�ص� �ı����� �ʰ� �Ѵ�.
                }
                _instance = obj.GetComponent<GameManager>();
            }

            return _instance;
        }
    }
}
