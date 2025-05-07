using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Object")]
    [SerializeField] private GameObject _target;

    [Header("Moving Settings")]
    [SerializeField] private float _cameraMoveSpeed;
    [SerializeField] private Vector3 _cameraPositionOffset;
    [SerializeField] private float _cameraBottomMin;

    void Update()
    {
        if (_target != null)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        var targetPos = _target.transform.position;
        targetPos.y = targetPos.y > _cameraBottomMin ? targetPos.y : _cameraBottomMin;

        var step = Vector2.Lerp(
            gameObject.transform.position,
            targetPos,
            _cameraMoveSpeed * Time.deltaTime * Vector2.Distance(gameObject.transform.position, targetPos));
        _cameraPositionOffset.y = step.y;
        gameObject.transform.position = _cameraPositionOffset;
    }
}
