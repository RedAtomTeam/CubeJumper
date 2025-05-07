using UnityEngine;

public class ParticlesHandler : MonoBehaviour
{
    [Header("Target Movement System")]
    [SerializeField] private PlayerMovementSystem _playerMovementSystem;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _wallSlideParticlesLeft;
    [SerializeField] private ParticleSystem _wallSlideParticlesRight;
    [SerializeField] private ParticleSystem _wallMoveParticlesLeft;
    [SerializeField] private ParticleSystem _wallMoveParticlesRight;
    [SerializeField] private ParticleSystem _particleSystemJump;
    [SerializeField] private ParticleSystem _particleSystemJumpLeft;
    [SerializeField] private ParticleSystem _particleSystemJumpRight;

    [Header("Particles Offset")]
    [SerializeField] private float particleSpawnOffset = 0f;

    void Start()
    {
        _playerMovementSystem.movingEvent += MovingHandle;
        _playerMovementSystem.slidingEvent += SlidingHandle;
        _playerMovementSystem.jumpEvent += JumpHandle;
    }

    private void MovingHandle(bool isMovingRight, bool isMovingLeft)
    {
        if (isMovingRight)
        {
            if (!_wallMoveParticlesRight.isPlaying)
                _wallMoveParticlesRight.Play();
            _wallMoveParticlesLeft.Stop();
        }
        else
            _wallMoveParticlesRight.Stop();

        if (isMovingLeft)
        {
            if (!_wallMoveParticlesLeft.isPlaying)
                _wallMoveParticlesLeft.Play();
            _wallMoveParticlesRight.Stop();
        }
        else
            _wallMoveParticlesLeft.Stop();
    }

    private void SlidingHandle(bool isWallLeftSliding, bool isWallRightSliding)
    {
        if (isWallLeftSliding)
        {
            if (!_wallSlideParticlesLeft.isPlaying)
                _wallSlideParticlesLeft.Play();
            _wallSlideParticlesRight.Stop();
        }
        else
            _wallSlideParticlesLeft.Stop();

        if (isWallRightSliding)
        {
            if (!_wallSlideParticlesRight.isPlaying)
                _wallSlideParticlesRight.Play();
            _wallSlideParticlesLeft.Stop();
        }
        else
            _wallSlideParticlesRight.Stop();
    }

    private void JumpHandle(bool isOnGround, bool isOnWallLeft, bool isOnWallRight)
    {
        if (isOnGround) 
            _particleSystemJump.Play();
        if (isOnWallLeft)
            _particleSystemJumpLeft.Play();
        if (isOnWallRight)
            _particleSystemJumpRight.Play();
    }

}
