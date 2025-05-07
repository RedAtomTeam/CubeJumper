using System;
using UnityEngine;

public class PlayerMovementSystem : MonoBehaviour
{
    [SerializeField] private PlayerMovementSystemConfig config;

    private float _horizontalMoveSpeed;
    private float _acceleration;
    private float _deceleration;
    private float _velPower;

    private float _jumpForce;
    private float _fallMultiplier; 
    private float _lowJumpMultiplier; 

    private float _wallSlideSpeed;
    private float _wallJumpForce;
    private Vector2 _wallJumpDirection;

    private LayerMask _groundLayer;
    private LayerMask _wallLayer;

    private bool _isGrounded;
    private bool _isOnWall;
    private bool _isWallLeft;
    private bool _isWallRight;
    private bool _isWallSliding;
    private bool _canHorizontalMove = true;
    private bool _canVerticalMove = false;

    private Rigidbody2D _rb;

    public bool jumpInput;
    public float verticalInput;
    public float horisontalInput;

    public event Action<bool, bool> movingEvent;
    public event Action<bool, bool> slidingEvent;
    public event Action<bool, bool, bool> jumpEvent;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        InitConfig();
    }

    private void InitConfig()
    {
        _horizontalMoveSpeed = config.horizontalMoveSpeed + UpgradeSystem.Instance.SpeedValue;
        _acceleration = config.acceleration;
        _deceleration = config.deceleration;
        _velPower = config.velPower;

        _jumpForce = config.jumpForce + UpgradeSystem.Instance.JumpValue;
        _fallMultiplier = config.fallMultiplier;
        _lowJumpMultiplier = config.lowJumpMultiplier;

        _wallSlideSpeed = config.wallSlideSpeed + UpgradeSystem.Instance.SlideValue;
        _wallJumpForce = config.wallJumpForce + UpgradeSystem.Instance.SpeedValue;
        _wallJumpDirection = config.wallJumpDirection;

        _groundLayer = config.groundLayer;
        _wallLayer = config.wallLayer;
    }

    private void Update()
    {
        IsOnGround();
        IsOnWall();

        if (jumpInput)
        {
            if (_isGrounded)
                Jump();
            else if (_isOnWall)
                WallJump();
        }

        if (_rb.velocity.y < 0) 
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        else if (_rb.velocity.y > 0 && !jumpInput) 
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;

        if (_isOnWall && !_isGrounded && _rb.velocity.y < 0f)
        {
            _isWallSliding = true;
        }
        else
            _isWallSliding = false;
    }

    private void IsOnGround()
    {
        var groundPointA = new Vector2(
            gameObject.transform.position.x - gameObject.transform.localScale.x / 2 + 0.1f,
            gameObject.transform.position.y + gameObject.transform.localScale.y / 2 - 0.1f
            );
        var groundPointB = new Vector2(
            gameObject.transform.position.x + gameObject.transform.localScale.x / 2 - 0.1f,
            gameObject.transform.position.y - gameObject.transform.localScale.y / 2 - 0.1f
            );

        _isGrounded = Physics2D.OverlapArea(groundPointA, groundPointB, _groundLayer);
    }

    private void IsOnWall()
    {
        var wallPointA = new Vector2(
            gameObject.transform.position.x + gameObject.transform.localScale.x / 2,
            gameObject.transform.position.y + gameObject.transform.localScale.y / 2 - 0.1f
            );
        var wallPointB = new Vector2(
            gameObject.transform.position.x + gameObject.transform.localScale.x / 2 + 0.1f,
            gameObject.transform.position.y - gameObject.transform.localScale.y / 2 + 0.1f
            );

        _isWallRight = Physics2D.OverlapArea(wallPointA, wallPointB, _wallLayer) && !_isGrounded;

        wallPointA = new Vector2(
            gameObject.transform.position.x - gameObject.transform.localScale.x / 2,
            gameObject.transform.position.y + gameObject.transform.localScale.y / 2 - 0.1f
            );
        wallPointB = new Vector2(
            gameObject.transform.position.x - gameObject.transform.localScale.x / 2 - 0.1f,
            gameObject.transform.position.y - gameObject.transform.localScale.y / 2 + 0.1f
            );

        _isWallLeft = Physics2D.OverlapArea(wallPointA, wallPointB, _wallLayer) && !_isGrounded;

        _isOnWall = (_isWallLeft || _isWallRight);

        if ( _isOnWall == true && _canVerticalMove == false) 
        { 
            _rb.velocity = Vector2.zero;
        }
        _canVerticalMove = _isOnWall;
    }


    private void FixedUpdate()
    {
        if (_canVerticalMove) {
            float targetSpeed = verticalInput * _wallSlideSpeed;
            float speedDif = targetSpeed - _rb.velocity.y;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velPower) * Mathf.Sign(speedDif);

            _rb.AddForce(movement * Vector2.up);

        }
        else if (_canHorizontalMove)
        {
            float targetSpeed = horisontalInput * _horizontalMoveSpeed;
            float speedDif = targetSpeed - _rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _deceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velPower) * Mathf.Sign(speedDif);
            _rb.AddForce(movement * Vector2.right);
        }
        slidingEvent?.Invoke(_isWallSliding && _isWallLeft, _isWallSliding && _isWallRight);
        movingEvent?.Invoke(_rb.velocity.x > 0.1f && _isGrounded, _rb.velocity.x < -0.1f && _isGrounded);
    }

    private void Jump()
    {
        jumpEvent?.Invoke(_isGrounded, _isWallLeft, _isWallRight);
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }

    private void WallJump()
    {
        jumpEvent?.Invoke(_isGrounded, _isWallLeft, _isWallRight);
        _canHorizontalMove = false;
        var velocityVector = new Vector2(_wallJumpDirection.x * _wallJumpForce, _wallJumpDirection.y * _wallJumpForce);
        if (_isWallRight)
            velocityVector.x *= -1; 
        
        _rb.velocity = velocityVector;
        _canHorizontalMove = true;
    }
}
