using System;
using UnityEngine;

public class PlayerLifeChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _damagedLayer;
    [SerializeField] private GameObject dieParticles;
 
    private bool _isLive = true;

    public event Action dieEvent;

    public bool IsLive {
        get => _isLive;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((_damagedLayer & (1 << other.gameObject.layer)) != 0)
        {
            _isLive = false;
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_damagedLayer & (1 << collision.gameObject.layer)) != 0)
        {
            _isLive = false;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(dieParticles, null).transform.position = transform.position;

        dieEvent?.Invoke();
    }

}
