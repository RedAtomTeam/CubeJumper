using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovementSystem _playerMovementSystem;

    [SerializeField] private Animator _animator;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _spriteMoveLeft;
    [SerializeField] private Sprite _spriteMoveRight;


    // Start is called before the first frame update
    void Start()
    {
        _playerMovementSystem.jumpEvent += DoJumpAnimation;
        _playerMovementSystem.movingEvent += DoHorisontalMoveAnimation;
        _playerMovementSystem.slidingEvent += DoSlideAnimation;
        _playerMovementSystem.landingEvent += DoLandingAnimation;
    }

    private void DoJumpAnimation(bool isGround, bool isJumpLeft, bool isJumpRight)
    {
        if (isGround)
        {
            _animator.SetTrigger("Jump");
        }
        if (isJumpLeft)
        {
            _animator.SetTrigger("WallJumpLeft");
            _animator.SetBool("IsOnWallRight", false);

        }
        if (isJumpRight)
        {
            _animator.SetTrigger("WallJumpRight");
            _animator.SetBool("IsOnWallLeft", false);
        }
    }

    private void DoLandingAnimation(bool isGroundLanding, 
                                    bool isOnWallLeftLanding, 
                                    bool isOnWallRightLanding)
    {
        _animator.SetBool("IsOnGround", isGroundLanding);
        _animator.SetBool("IsOnWallLeft", isOnWallLeftLanding);
        _animator.SetBool("IsOnWallRight", isOnWallRightLanding);

        if (isGroundLanding || isOnWallLeftLanding || isOnWallRightLanding)
        {
            _animator.SetTrigger("Landing");
            //print($"Landing: {isGroundLanding} - {isOnWallLeftLanding} - {isOnWallRightLanding}");
        }
    }

    private void DoHorisontalMoveAnimation(bool isGround, bool isRightMove, bool isLeftMove)
    {
        if (isGround)
        {
            _animator.SetBool("IsMoveLeft", isLeftMove);
            _animator.SetBool("IsMoveRight", isRightMove);
        }
    }

    private void DoSlideAnimation(bool isSlidingLeft, bool isSlidingRight)
    {

    }
}
