using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Snake))]
public class SnakeSizeView : MonoBehaviour
{
    [SerializeField] private TMP_Text view;
    private Snake _snake;

    private void Awake()
    {
        _snake = GetComponent<Snake>();
    }

    private void OnEnable()
    {
        _snake.SizeUpdated += OnSizeUpdated;
    }

    private void OnDisable()
    {
        _snake.SizeUpdated -= OnSizeUpdated;
    }

    private void OnSizeUpdated(int tail)
    {
        view.text = tail.ToString();
    }
}
