using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Animation : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                panel.SetActive(false);
            }
        }
    }
}
