using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnedObjectPool : MonoBehaviour
{
    [SerializeField] private SpawnedObject _spawnedObjectPref;
    [SerializeField] private int _spawnCountAtStart;
    [SerializeField] private Vector3 _startSpawnPosition;
    
    private readonly Queue<SpawnedObject> _objectPool = new();

    [SerializeField] private List<SpawnedObject> _spawnedObjects = new List<SpawnedObject>();

    public Vector2 SpawnedObjectSize => _spawnedObjectPref.gameObject.GetComponent<BoxCollider2D>().size;

    private void Start()
    {
        for (int i = 0; i < _spawnCountAtStart; i++)
        {
            var obj = CreateNew();
            obj.gameObject.transform.position = _startSpawnPosition;
            Return(obj);
        }
    }

    public Bounds GetBounds()
    {
        SpawnedObject obj = Get();

        Collider2D[] colliders;

        colliders = obj.gameObject.GetComponentsInChildren<Collider2D>();
        if (colliders.Length == 0)
            return new Bounds(_spawnedObjectPref.transform.position, Vector3.zero);
        Bounds totalBounds = colliders[0].bounds;
        for (int i = 1; i < colliders.Length; i++)
        {
            totalBounds.Encapsulate(colliders[i].bounds);
        }

        Return(obj);
        return totalBounds;
    }

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
        spawnedObject.gameObject.SetActive(false);  
        _objectPool.Enqueue(spawnedObject);
        _spawnedObjects.Add(spawnedObject);
    } 
}
