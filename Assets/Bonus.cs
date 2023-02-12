using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bonus : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Vector2Int _bonusSizeRange;
    private int _bonusSize;

    private void Start()
    {
        _bonusSize = Random.Range(_bonusSizeRange.x, _bonusSizeRange.y);
        _text.text = _bonusSize.ToString();
    }

    public int Collect()
    {
        Destroy(gameObject);
        return _bonusSize;
    }
}
