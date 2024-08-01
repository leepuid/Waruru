using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Burst.CompilerServices;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //[SerializeField] private Transform Player;
    //[SerializeField] private GameObject Cube;

    //private float xDistance;
    //private float zDistance;
    //private float CubeScale;

    //Vector3 xPos;
    //Vector3 zPos;
    //void Start()
    //{
    //    CubeScale = Cube.transform.localScale.x;
    //}

    //void Update()
    //{
    //    bool CreateX = false;
    //    bool CreateZ = false;
    //    xDistance = Player.position.x - transform.position.x;
    //    zDistance = Player.position.z - transform.position.z;
    //    if (Mathf.Abs(xDistance) > CubeScale/4)
    //    {
    //        Vector3 xDir = new Vector3(xDistance, 0, 0).normalized;
    //        xPos = xDir * CubeScale;
    //        CreateX = true;
    //        CreateMap(transform.position + xPos);
    //    }
    //    if (Mathf.Abs(zDistance) > CubeScale/4)
    //    {
    //        Vector3 zDir = new Vector3(0, 0, zDistance).normalized;
    //        zPos = zDir * CubeScale;
    //        CreateZ = true;
    //        CreateMap(transform.position + zPos);
    //    }
    //    if(CreateX&&CreateZ)
    //    {
    //       CreateMap(transform.position + xPos + zPos);
    //    }
    //    if (Mathf.Abs(xDistance) > CubeScale/2 +1)
    //        transform.position += xPos;
    //    if (Mathf.Abs(zDistance) > CubeScale/2 +1)
    //        transform.position += zPos;
    //}

    //private void CreateMap(Vector3 pos)
    //{
    //    Vector3 DownPos = pos - Vector3.up * 5;
    //    // Debug.DrawRay(DownPos, Vector3.up * CubeScale);
    //    if (!Physics.Raycast(DownPos, Vector3.up, CubeScale))
    //    {
    //        Instantiate(Cube, pos, Quaternion.identity);
    //    }
    //}

}
