using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform _container;
    [SerializeField] private int _repeatCount;
    [SerializeField] private int _distanceBeatweenFullLine;
    [SerializeField] private int _distanceBeatweenRandomLine;
    [Header("Block")]
    [SerializeField] private Block _blockTamplate;
    [SerializeField] private int _blockSpawnChance;
    [Header("Wall")] 
    [SerializeField] private Wall _wallTamplate;
    [SerializeField] private int _wallSpawnChance;

    private BlockSpawnPoint[] _blockSpawnPoint;
    private WallSpawnPoint[] _wallSpawnPoints;
    private void Start()
    {
        _blockSpawnPoint = GetComponentsInChildren<BlockSpawnPoint>();
        _wallSpawnPoints = GetComponentsInChildren<WallSpawnPoint>();

        for (int i = 0; i < _repeatCount; i++)
        {
            MoveSpawner(_distanceBeatweenFullLine);
            GenerateFullLineElements(_blockSpawnPoint, _blockTamplate.gameObject);
            GenerateRandomLineElements(_wallSpawnPoints, _wallTamplate.gameObject, _wallSpawnChance,
                _distanceBeatweenFullLine, _distanceBeatweenFullLine / 2f);
            MoveSpawner(_distanceBeatweenRandomLine);
            GenerateRandomLineElements(_blockSpawnPoint, _blockTamplate.gameObject, _blockSpawnChance);
            GenerateRandomLineElements(_wallSpawnPoints, _wallTamplate.gameObject, _wallSpawnChance, 
                _distanceBeatweenRandomLine, _distanceBeatweenRandomLine / 2f);
        }
    }

    private void GenerateFullLineElements(SpawnPoint[] spawnPoints, GameObject _blockTamplate)
    {
        for (int i = 0; i < _blockSpawnPoint.Length; i++)
        {
            GenerateElement(spawnPoints[i].transform.position, _blockTamplate);
        }
    }

    private void GenerateRandomLineElements(SpawnPoint[] spawnPoints, GameObject blockTamplate, int blockSpawnChance, int scaleY = 1, float offsetY = 0)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Random.Range(0, 100) < blockSpawnChance)
            {
                GameObject element = GenerateElement(spawnPoints[i].transform.position, blockTamplate, offsetY);
                element.transform.localScale = new Vector3(element.transform.localScale.x,
                    scaleY, element.transform.localScale.z);
            }
        }
    }

    private GameObject GenerateElement(Vector3 spawmPoint, GameObject element, float offsetY = 0)
    {
        spawmPoint.y -= offsetY;
        return Instantiate(element, spawmPoint, quaternion.identity, _container);
    }

    private void MoveSpawner(int distanceY)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.z);
    }
}
