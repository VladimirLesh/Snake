using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private Vector2Int _destroyPriceRange;
    [SerializeField] private Color[] _colors;
    private int _destroyPrice;
    private int filling;
    private SpriteRenderer _spriteRenderer;

    public event UnityAction<int> FillingUpdated;
    public int LeftToFill => _destroyPrice - filling;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor(_colors[Random.Range(0, _colors.Length)]);
        _destroyPrice = Random.Range(_destroyPriceRange.x, _destroyPriceRange.y);
        FillingUpdated?.Invoke(LeftToFill);
    }

    public void Fill()
    {
        filling++;
        FillingUpdated?.Invoke(LeftToFill);

        if (filling == _destroyPrice)
        {
            Destroy(gameObject);
        }
    }

    private void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
