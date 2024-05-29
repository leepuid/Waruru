using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotchSafe : MonoBehaviour
{
    RectTransform _rectTransform;
    Rect _safeArea;
    Vector2 _minAnchor;
    Vector2 _maxAnchor;
    private void Awake()
    {
        RectTransform _rectTransform = GetComponent<RectTransform>();
        Rect _safeArea = Screen.safeArea;
        _minAnchor = _safeArea.position;
        _maxAnchor = _safeArea.size;
        _minAnchor.x/=Screen.width;
        _minAnchor.y/=Screen.height;
        _maxAnchor.x/=Screen.width;
        _maxAnchor.y/=Screen.height;

        _rectTransform.anchorMin = _minAnchor;
        _rectTransform .anchorMax = _maxAnchor;
    }
}
