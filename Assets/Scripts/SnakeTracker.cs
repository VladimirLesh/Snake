using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using FixedUpdate = UnityEngine.PlayerLoop.FixedUpdate;

public class SnakeTracker : MonoBehaviour
{
    [SerializeField] private SnakeHead _snakeHead;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, GetTargetPosition(), _speed * Time.fixedDeltaTime);
    }

    private Vector3 GetTargetPosition()
    {
        return new Vector3(transform.position.x, _snakeHead.transform.position.y + _offsetY, transform.position.z);
    }
}
