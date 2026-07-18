using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;

    //private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Start()
    {
        //animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleVisual();
    }

    private void HandleVisual()
    {
        //animator.SetFloat("MoveX", moveInput.x);
        //animator.SetFloat("MoveY", moveInput.y);

        //bool isMoving = moveInput.sqrMagnitude > 0;
        //animator.SetBool("IsMoving", isMoving);

        if (moveInput.x < 0)
            spriteRenderer.flipX = true;
        else if (moveInput.x > 0)
            spriteRenderer.flipX = false;
    }

    private void HandleMovement()
    {
        //transform.position += (Vector3)moveInput * moveSpeed * Time.deltaTime;

        rb.linearVelocity = moveInput * moveSpeed;

        //Debug.Log(moveInput);


    }

    private void HandleInput()
    {
        moveInput = GameInput.Instance.GetMovementVectorNormalized();
    }
}
