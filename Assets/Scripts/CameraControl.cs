using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    #region Singleton

    private static CameraControl _instance;
    private static bool _initialized;

    public static CameraControl Instance
    { // 읽기 전용 프로퍼티.
        get
        {
            if (!_initialized)
            { // 초기화가 되지 않았다면 초기화.
                _initialized = true;

                // 오브젝트를 찾아 넣어준다.
                GameObject obj = GameObject.Find("@CameraControl");
                if (obj == null)
                { // 오브젝트가 없다면 만든다.
                    obj = new() { name = "@CameraControl" };
                    obj.AddComponent<CameraControl>();
                    DontDestroyOnLoad(obj); // 씬을 전환해도 파괴되지 않게 한다.
                }
                _instance = obj.GetComponent<CameraControl>();
            }

            return _instance;
        }
    }

    #endregion

    private CinemachineVirtualCamera _camera;

    public void SetTarget(Transform target)
    {
        if (_camera == null)
        {
            _camera = GameObject.FindWithTag("VCamera").GetComponent<CinemachineVirtualCamera>();
        }
        _camera.Follow = target;
    }
}
