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
    { // �б� ���� ������Ƽ.
        get
        {
            if (!_initialized)
            { // �ʱ�ȭ�� ���� �ʾҴٸ� �ʱ�ȭ.
                _initialized = true;

                // ������Ʈ�� ã�� �־��ش�.
                GameObject obj = GameObject.Find("@CameraControl");
                if (obj == null)
                { // ������Ʈ�� ���ٸ� �����.
                    obj = new() { name = "@CameraControl" };
                    obj.AddComponent<CameraControl>();
                    DontDestroyOnLoad(obj); // ���� ��ȯ�ص� �ı����� �ʰ� �Ѵ�.
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
