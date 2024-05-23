using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domino : MonoBehaviour
{
    [SerializeField] GameObject dominoPrefab;

    private Vector3 _rotation;
    private Rigidbody _dominoRb;

    private float _rotationDomino = 90f;
    private int _turnDirection = 1;
    private float _rotationYTime = 1.0f;

    private bool _isClickY = false;
    private bool _isClickZ = false;
    private bool _isSpawn = false;

    private void Awake()
    {
        CameraControl.ins.SetTarget(transform);
    }

    private void Start()
    {
        _rotation = transform.eulerAngles;
        _dominoRb = GetComponent<Rigidbody>();
        _dominoRb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (Main.Game._gameState != GameState.Play)
            return;

        RotationYDomino();
        RotationZDomino();
    }

    private void Update()
    {
        if (Main.Game._gameState != GameState.Play)
            return;

        _rotationYTime -= Time.deltaTime;
        if(_rotationYTime < 0)
        {
            _turnDirection *= -1;
            _rotationYTime = 2.0f;
        }

        if (Input.GetMouseButtonDown(0)) // TODO : 빌드 시 모바일 터치로 바꾸기.
        {
            if (_isClickY)
            {
                _isClickZ = true;
            }
            _isClickY = true;

            if (_isClickZ)
            {
                Drop();
                if (!_isSpawn)
                {
                    SpawnDomino();
                    _isSpawn = true;
                }
            }
        }
    }

    private void RotationYDomino()
    {
        if (!_isClickY)
        {
            _rotation = _rotation + new Vector3(0.0f, _rotationDomino * _turnDirection, 0.0f) * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(_rotation);
        }
    }

    private void RotationZDomino()
    {
        if (!_isClickY)
        {
            return;
        }
        if (!_isClickZ)
        {
            _rotation = _rotation + new Vector3(0.0f, 0.0f, _rotationDomino) * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(_rotation);
        }
    }

    private void Drop()
    {
        _dominoRb.isKinematic = false;
    }

    private void SpawnDomino()
    {
        Instantiate(dominoPrefab, transform.position + transform.forward, Quaternion.identity);
    }
}
