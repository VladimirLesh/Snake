using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] private int _springiness;
    [SerializeField] private int _speed;
    [SerializeField] private int _tailSize;
    private List<Segment> _tail;
    private TailGenerator _tailGenerator;
    private SnakeInput _snakeInput;

    public event UnityAction<int> SizeUpdated;

    private void Awake()
    {
        _tailGenerator = GetComponent<TailGenerator>();
        _tail = _tailGenerator.Generate(_tailSize);
        _snakeInput = GetComponent<SnakeInput>();
    }

    private void OnEnable()
    {
        _head.BlockCollided += OnBlockCollided;
        _head.BonusCollected += OnBonusCollected;
    }

    private void OnDisable()
    {
        _head.BlockCollided -= OnBlockCollided;
        _head.BonusCollected -= OnBonusCollected;

    }

    private void Start()
    {
        SizeUpdated?.Invoke(_tail.Count);
    }

    private void FixedUpdate()
    {
        Move(_head.transform.position + _head.transform.up * _speed * Time.deltaTime);
        _head.transform.up = _snakeInput.GetDirectionToClick(_head.transform.position);
    }


    private void Move(Vector3 nextPosition)
    {
        Vector3 previousPosition = _head.transform.position;

        foreach (var segment in _tail)
        {
            Vector3 segmentPosition = segment.transform.position;
            segment.transform.position = Vector2.Lerp(segment.transform.position, previousPosition,
                _springiness * Time.deltaTime);
            previousPosition = segmentPosition;
        }
        _head.Move(nextPosition);
    }

    private void OnBlockCollided()
    {
        if (_tail.Count >= 1)
        {
            Segment deletedSegment = _tail[_tail.Count - 1];
            _tail.Remove(deletedSegment);
            Destroy(deletedSegment.gameObject);
            SizeUpdated?.Invoke(_tail.Count);
        }
        else
        {
            Destroy(_head.gameObject);
            Destroy(gameObject);
            
        }
    }

    private void OnBonusCollected(int bonusCount)
    {
        _tail.AddRange(_tailGenerator.Generate(bonusCount));
        SizeUpdated?.Invoke(_tail.Count);
    }
}
