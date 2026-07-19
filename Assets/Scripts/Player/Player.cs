using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    private Vector2 moveInput;


    private void FixedUpdate()
    {
        HandleMovement(moveInput , speed);
    }
    private void Update()
    {
        HandleInput();
        HandleVisual(moveInput);
    }

    private void HandleInput()
    {
        moveInput = PlayerInput.Instance.GetMovementVectorNormalized();
    }

    private void HandleMovement(Vector2 moveInput, float speed)
    {
        PlayerMovement.Instance.Move(moveInput , speed);
    }

    private void HandleVisual(Vector2 moveInput)
    {
        PlayerVisual.Instance.UpdateVisual(moveInput);
    }
}
