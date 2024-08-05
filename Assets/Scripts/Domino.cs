//using GooglePlayGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domino : MonoBehaviour
{
    [SerializeField] GameObject dominoPrefab;
    [SerializeField] GameObject flipDominoPrefab;

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
        flipDominoPrefab.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _rotationSpeed = Main.Game.GetSpeed();
        Debug.Log(_rotationSpeed);
        _rotationTime /= _rotationSpeed;
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
            Main.Game._gameState = GameState.Over;
            // Waruru! 넘어진 업적 달성.
            //PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_waruru, 100, (bool success) => { });
            StartCoroutine(CheckDominoDown());
        }
    }

    private IEnumerator CheckDominoDown()
    {
        Main.Game._timer = 0.0f;

        while (Main.Game._timer < 5.0f)
        {
            Main.Game._timer += Time.deltaTime;
            yield return null;
        }

        Main.Game._gameState = GameState.End;
    }

    private void GetPoint()
    {
        Main.Game.AddScore();
    }

    private void Touch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isClickY)
            {
                _isClickZ = true;
            }
            _isClickY = true;
            _rotationTime = 1.0f / _rotationSpeed;

            if (_isClickZ)
            {
                Drop();
                if (!_isSpawn)
                {
                    _spawnPosition = transform.position + transform.forward;
                    _spawnRotation = transform.rotation;
                    FlipYDomino();
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

    private void FlipYDomino()
    {
        if (transform.rotation.eulerAngles.z > 180)
        {
            float newYRotation = transform.rotation.eulerAngles.y + 180;
            float newZRotation = (360 - transform.rotation.eulerAngles.z) % 360;
            flipDominoPrefab.transform.rotation = Quaternion.Euler(
                            transform.rotation.eulerAngles.x, newYRotation, newZRotation);
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

    //public float hueIncrement = 0.01f; // Hue 증가 값 설정
    //private void ChangeColor()
    //{
    //    Renderer rd = gameObject.GetComponent<Renderer>();
    //    float hue = Main.Game.GetHue();
    //    //rd.material.SetColor("Color_d3f90b46fa4040c48d4031973961bef6", randomColor);
    //    Color currentColor = rd.material.GetColor("_ColorTop");

    //    //여기에서 색을 변환하는 과정을 넣어줘
    //    Color.RGBToHSV(currentColor, out float tmp, out float saturation, out float value);
    //    Debug.Log($"휴..{hue}, {saturation}");

    //    // 새로운 색상을 HSV에서 RGB로 변환
    //    currentColor = Color.HSVToRGB(hue, saturation, value);

    //    //currentColor = new Color(r, g, b);
    //    rd.material.SetColor("_ColorTop", currentColor);
    //}

    IEnumerator CoSpawnWaiting()
    {
        while (!_dominoRb.IsSleeping())
        {
            yield return null;
            if (Main.Game._gameState == GameState.Over || Main.Game._gameState == GameState.End)
                yield break;
        }
        if (!_isFallDown) GetPoint();
        SpawnDomino();
    }
}
