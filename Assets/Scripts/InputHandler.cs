using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovementSystem _playerMovementSystem;

    private void Update()
    {
        _playerMovementSystem.horisontalInput = Input.GetAxisRaw("Horizontal");
        _playerMovementSystem.verticalInput = Input.GetAxisRaw("Vertical");
        _playerMovementSystem.jumpInput = Input.GetKeyDown(KeyCode.Space);

    }
}
