using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerMovementSystemConfig")]
public class PlayerMovementSystemConfig : ScriptableObject
{
    [Header("Movement")]
    public float horizontalMoveSpeed;
    public float acceleration;
    public float deceleration;
    public float velPower;

    [Header("Jumping")]
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    [Header("Wall Slide & Jump")]
    public float wallSlideSpeed;
    public float wallJumpForce;
    public Vector2 wallJumpDirection;

    [Header("Ground & Wall Check")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;
}
