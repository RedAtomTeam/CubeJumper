using System.Collections.Generic;
using UnityEngine;

public class SpawnedObjectPool : MonoBehaviour
{
    [SerializeField] private SpawnedObject _spawnedObjectPref;
    
    private readonly Queue<SpawnedObject> _objectPool = new();

    public Vector2 SpawnedObjectSize => _spawnedObjectPref.gameObject.GetComponent<BoxCollider2D>().size;

    public SpawnedObject Get()
    {
        if (_objectPool.Count < 1)
        {
            var newObject = CreateNew();
            newObject.Init(this);
            return newObject;
        }
        var returnedObject = _objectPool.Dequeue();
        returnedObject.gameObject.SetActive(true);
        return returnedObject;
    }

    private SpawnedObject CreateNew()
    {
        return Instantiate(_spawnedObjectPref);
    }

    public void Return(SpawnedObject spawnedObject)
    {
        _objectPool.Enqueue(spawnedObject);
    } 
}
