using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    [SerializeField] private int _value = 1;
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private GameObject parent;
    [SerializeField] private Vector3 target;
    [SerializeField] private float _animationDuration;

    private BalanceManager balanceManager;


    private bool _isCollected = false;

    private void Awake()
    {
        balanceManager = BalanceManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        if (!_isCollected)
        {
            _isCollected = true;
            balanceManager.AddBalance(_value);
            SoundEffectsManager.Instance.PlayOneShot(_collectSound);
            parent.transform.DOJump(parent.transform.position, 2f, 1, _animationDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _isCollected = false;
                    parent.GetComponent<SpawnedObject>().Return();
                });
        }
    }

}
