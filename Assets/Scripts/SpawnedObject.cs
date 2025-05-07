using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class SpawnedObject : MonoBehaviour
{
    [Header("Physics Settings")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _dragAfterBottomEnter;
    [SerializeField] private float _defaultDrag;
    [SerializeField] private LayerMask _obstacleLayer;

    private SpawnedObjectPool _pool;

    private void Start()
    {
        _rb.drag = _defaultDrag;
    }

    public void Init(SpawnedObjectPool pool)
    {
        _pool = pool;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((_obstacleLayer & (1 << other.gameObject.layer)) != 0)
        {
            _rb.drag = _dragAfterBottomEnter;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((_obstacleLayer & (1 << other.gameObject.layer)) != 0)
        {
            Return();
        }
    }

    public void Return()
    {
        gameObject.SetActive(false);
        _rb.drag = _defaultDrag;
        if (_pool == null)
            Destroy(_pool);
        else
            _pool.Return(this);
    }
}
