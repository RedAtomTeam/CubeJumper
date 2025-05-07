using UnityEngine;

public class PlayerSoundEffectsHandler : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerMovementSystem _movementSystem;
    [SerializeField] private PlayerLifeChecker _playerLifeChecker;

    [Header("Audio Effects")]
    [SerializeField] private AudioSource _jumpAudioSource;
    [SerializeField] private AudioSource _slideAudioSource;
    [SerializeField] private AudioSource _moveAudioSource;
    [SerializeField] private AudioSource _dieAudioSource;

    

    private void Start()
    {
        _movementSystem.jumpEvent += JumpEffect;
        _movementSystem.slidingEvent += SlideEffect;
        _movementSystem.movingEvent += MoveEffect;
        _playerLifeChecker.dieEvent += DieEffect;
    }

    private void JumpEffect(bool isOnWall, bool isWallLeft, bool isWallRight)
    {
        _jumpAudioSource.Play();
    }

    private void SlideEffect(bool isWallLeft, bool isWallRight)
    {
        if (!_slideAudioSource.isPlaying)
            _slideAudioSource.Play();
    }

    private void MoveEffect(bool isMoveLeft, bool isMoveRight)
    {
        if (!_moveAudioSource.isPlaying)
            _moveAudioSource.Play();
    }

    private void DieEffect()
    {
        _dieAudioSource.Play();
    }

}
