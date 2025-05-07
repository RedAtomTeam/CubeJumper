using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    [SerializeField] private int _value = 1; // Количество валюты за подбор
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private GameObject parent;
    [SerializeField] private Vector3 target;
    [SerializeField] private float _animationDuration;


    private bool _isCollected = false;

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
            BalanceManager.Instance.AddBalance(_value);
            SoundEffectsManager.Instance.PlayOneShot(_collectSound);
            print("Start animation");
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
