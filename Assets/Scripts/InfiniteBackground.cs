using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [Header("ParallaxEffectsSettings")]
    [SerializeField] private GameObject _parallaxSegmentPref;
    [SerializeField] private Camera _targetCamera;
    [SerializeField] private int _orderInLayer;
    [SerializeField] private float _parallaxEffectSpeed;

    private GameObject[] _parallaxSegments = new GameObject[2];
    private float _startPosition;
    private int _cameraSegment = 0;

    private void Start()
    {
        _startPosition = transform.position.y;

        for (int i = 0; i < _parallaxSegments.Length; i++)
        {
            _parallaxSegments[i] = Instantiate(_parallaxSegmentPref, gameObject.transform);
            _parallaxSegments[i].GetComponent<SpriteRenderer>().sortingOrder = _orderInLayer;
            _parallaxSegments[i].transform.localPosition = new Vector3(0, (_cameraSegment + i) * 12, 0);
        }
    }

    private void FixedUpdate()
    {
        float distance = (_targetCamera.transform.position.y * _parallaxEffectSpeed);
        transform.position = new Vector3(transform.position.x, _startPosition + distance, transform.position.z);

        _cameraSegment = CalculateCameraSegment();

        for (int i = 0; i < _parallaxSegments.Length; i++)
            _parallaxSegments[i].transform.localPosition = new Vector3(0, (_cameraSegment + i) * 12, 0);
    }

    int CalculateCameraSegment()
    {
        return (int)((_targetCamera.transform.position.y - transform.position.y) / 12);
    }
}
