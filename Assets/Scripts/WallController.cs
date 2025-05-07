using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] private GameObject _wallSegmentPref;
    [SerializeField] private Camera _mainCamera;

    private int _cameraSegment = 0;

    private GameObject[] _wallSegments = new GameObject[2];

    private void Start()
    {
        for (int i = 0; i < _wallSegments.Length; i++)
        {
            _wallSegments[i] = Instantiate(_wallSegmentPref, gameObject.transform);
            _wallSegments[i].transform.localPosition = new Vector3(0, (_cameraSegment+i)*12, 0);
        }
    }

    private void Update()
    {
        _cameraSegment = CalculateCameraSegment();
        
        for (int i = 0; i < _wallSegments.Length; i++)
        {
            _wallSegments[i].transform.localPosition = new Vector3(0, (_cameraSegment+i)*12, 0);
        }
    }

    int CalculateCameraSegment()
    {
        return (int)(_mainCamera.transform.position.y / 12);
    }
}
