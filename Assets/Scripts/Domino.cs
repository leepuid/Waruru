using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domino : MonoBehaviour
{
    [SerializeField] GameObject dominoPrefab;

    private Vector3 rotation;
    private Rigidbody dominoRb;

    private float rotationDomino = 90f;
    private int turnDirection = 1;
    private float rotationYTime = 1.0f;

    private bool isClickY = false;
    private bool isClickZ = false;
    private bool isSpawn = false;

    private void Awake()
    {
        CameraControl.Instance.SetTarget(transform);
    }

    private void Start()
    {
        rotation = transform.eulerAngles;
        dominoRb = GetComponent<Rigidbody>();
        dominoRb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        RotationYDomino();
        RotationZDomino();
    }

    private void Update()
    {
        rotationYTime -= Time.deltaTime;
        if(rotationYTime < 0)
        {
            turnDirection *= -1;
            rotationYTime = 2.0f;
        }

        if (Input.GetMouseButtonDown(0)) // TODO : 빌드 시 모바일 터치로 바꾸기.
        {
            if (isClickY)
            {
                isClickZ = true;
            }
            isClickY = true;

            if (isClickZ)
            {
                Drop();
                if (!isSpawn)
                {
                    SpawnDomino();
                    isSpawn = true;
                }
            }
        }
    }

    private void RotationYDomino()
    {
        if (!isClickY)
        {
            rotation = rotation + new Vector3(0.0f, rotationDomino * turnDirection, 0.0f) * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    private void RotationZDomino()
    {
        if (!isClickY)
        {
            return;
        }
        if (!isClickZ)
        {
            rotation = rotation + new Vector3(0.0f, 0.0f, rotationDomino) * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    private void Drop()
    {
        dominoRb.isKinematic = false;
    }

    private void SpawnDomino()
    {
        Instantiate(dominoPrefab, transform.position + transform.forward, Quaternion.identity);
    }
}
