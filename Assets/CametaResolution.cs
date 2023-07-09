using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CametaResolution : MonoBehaviour
{
    private Camera _camera;
    private float _defaultWegth;

    private void Start()
    {
        _camera = Camera.main;
        _defaultWegth = _camera.orthographicSize * _camera.aspect;
        Debug.Log(_camera.orthographicSize + " " + _camera.aspect);
}

    private void Update()
    {
        _camera.orthographicSize = _defaultWegth / _camera.aspect;
    }
}
