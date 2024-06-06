using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domino : MonoBehaviour
{
    [SerializeField] GameObject dominoPrefab;

    private Vector3 _rotation;
    private Vector3 _spawnPosition;
    private Quaternion _spawnRotation;
    private Rigidbody _dominoRb;
    private float _rotationSpeed;

    private float _rotationDomino = 90f;
    private int _turnDirection = 1;
    private float _rotationTime = 1.0f;

    private bool _isClickY = false;
    private bool _isClickZ = false;
    private bool _isSpawn = false;
    private bool _isFallDown = false;
    private void Awake()
    {
        // _rotationSpeed = UnityEngine.Random.Range(1.0f, 2.51f);
        _rotationSpeed = Main.Game.GetSpeed();
        Debug.Log(_rotationSpeed);
        _rotationTime /= _rotationSpeed;
        ChangeColor();
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

        RotationTime();
        Touch();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isFallDown)
        {
            _isFallDown = true;
            CameraControl.ins.SetTarget(transform, true);
            Main.Game._gameState = GameState.End;
        }
    }

    private void GetPoint()
    {
        Main.Game.AddScore();
    }

    private void Touch()
    {
        if (Input.GetMouseButtonDown(0)) // TODO : 빌드 시 모바일 터치로 바꾸기.
        {
            if (_isClickY)
            {
                _isClickZ = true;
            }
            _isClickY = true;
            _rotationTime = 1.0f/_rotationSpeed;

            if (_isClickZ)
            {
                Drop();
                if (!_isSpawn)
                {
                    _spawnPosition = transform.position + transform.forward;
                    _spawnRotation = transform.rotation;
                    if (transform.rotation.eulerAngles.z > 180)
                    {
                        transform.rotation = Quaternion.Euler(
                            transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z * -1);
                    }
                    CameraControl.ins.SetTarget(null);
                    StartCoroutine(CoSpawnWaiting());
                    _isSpawn = true;
                }
            }
        }
    }

    private void RotationTime()
    {
        _rotationTime -= Time.deltaTime;
        if (_rotationTime < 0)
        {
            _turnDirection *= -1;
            _rotationTime = 2.0f / _rotationSpeed;
        }
    }

    private void RotationYDomino()
    {
        if (!_isClickY)
        {
            _rotation = _rotation + new Vector3(0.0f, _rotationDomino * _turnDirection, 0.0f) * Time.fixedDeltaTime * _rotationSpeed;
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
            _rotation = _rotation + new Vector3(0.0f, 0.0f, _rotationDomino * _turnDirection) * Time.fixedDeltaTime * _rotationSpeed;
            transform.rotation = Quaternion.Euler(_rotation);
        }
    }

    private void Drop()
    {
        _dominoRb.isKinematic = false;
    }

    private void SpawnDomino()
    {
        Instantiate(dominoPrefab, _spawnPosition, _spawnRotation);
    }

    private void ChangeColor()
    {
        //Color currentColor = gameObject.GetComponent<MeshRenderer>().material.color;
        //float r = currentColor.r;
        //float g = currentColor.g;
        //float b = currentColor.b;
        //if (r == 255f && g == 155f && b < 155f)
        //{
        //    b += 10f;
        //}
        //if (r > 155f && g == 255f && b == 255f)
        //{
        //    r -= 10f;
        //}
        //if (r == 155f && g < 255f && b == 255f)
        //{
        //    g += 10f;
        //}
        //if (r == 155f && g == 155f && b < 155f)
        //{
        //    b += 10f;
        //}
        //if (r < 255f && g == 155f && b == 155f)
        //{
        //    r += 10f;
        //}
        //currentColor = new Color(r, g, b);
        Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        Material mat = gameObject.GetComponent<MeshRenderer>().material;
        mat.SetColor("Color_d3f90b46fa4040c48d4031973961bef6", randomColor);
    }

    IEnumerator CoSpawnWaiting()
    {
        while (!_dominoRb.IsSleeping())
        {
            yield return null;
            if (Main.Game._gameState == GameState.End)
                yield break;
        }
        if (!_isFallDown) GetPoint();
        SpawnDomino();
    }
}
