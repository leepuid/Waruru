using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : Singleton<CameraControl>
{

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
