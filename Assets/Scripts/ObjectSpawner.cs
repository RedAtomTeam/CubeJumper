using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    [Range(0.2f, 10f)]
    [SerializeField] private float _spawnTimeMin;
    [Range(0.3f, 20f)]
    [SerializeField] private float _spawnTimeMax;
    [SerializeField] private List<SpawnedObjectPool> _spawnedObjectsPools;

    [Header("Границы области")]
    [SerializeField] private Vector2 _areaSize;

    [Header("Слои препятствий")]
    [SerializeField] private LayerMask _obstacleLayerMask;

    private readonly Collider2D[] _obstacles = new Collider2D[32];


    private void Awake()
    {
        if (_spawnTimeMax <= _spawnTimeMin)
            throw new ArgumentException("spawnTimeMax should be greater than spawnTimeMin");
    }

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(UnityEngine.Random.Range(_spawnTimeMin, _spawnTimeMax));
        }
    }

    private void SpawnObject()
    {
        var targetPoolIndex = UnityEngine.Random.Range(0, _spawnedObjectsPools.Count);
        var objectSize = _spawnedObjectsPools[targetPoolIndex].SpawnedObjectSize;

        Vector3 spawnPosition = new (
            transform.position.x + UnityEngine.Random.Range(
                -_areaSize.x / 2 + objectSize.x / 2, 
                _areaSize.x / 2 - objectSize.x / 2),
            transform.position.y,
            transform.position.z
        );

        var pointA = new Vector2(spawnPosition.x - objectSize.x, spawnPosition.y + objectSize.y);
        var pointB = new Vector2(spawnPosition.x + objectSize.x, spawnPosition.y - objectSize.y);
        Physics2D.OverlapAreaNonAlloc(pointA, pointB, _obstacles, _obstacleLayerMask);

        if (Physics2D.OverlapAreaNonAlloc(pointA, pointB, _obstacles, _obstacleLayerMask) > 0)
            return;

        var spawnedObject = _spawnedObjectsPools[UnityEngine.Random.Range(0, _spawnedObjectsPools.Count)].Get();

        spawnedObject.gameObject.transform.position = spawnPosition;
    }
}
