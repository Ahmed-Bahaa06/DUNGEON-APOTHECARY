using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    private Rigidbody2D rb;
    private BoxCollider2D box;

    private bool canMove;
    public bool CanMove => canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameOver += Stop;
    }

    public void Stop()
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void Resume()
    {
        canMove = true;
    }

    public void Move(Vector2 moveInput, float moveSpeed)
    {
        if (!canMove) return;

        Vector2 moveDir = CalculateMovement(moveInput);
        rb.linearVelocity = moveDir * moveSpeed;
    }

    private Vector2 CalculateMovement(Vector2 input)
    {
        Vector2 movement = input;

        if (movement.x != 0)
        {
            Vector2 dir = new Vector2(Mathf.Sign(movement.x), 0);

            if (IsBlocked(dir, collisionMask))
                movement.x = 0;
        }

        if (movement.y != 0)
        {
            Vector2 dir = new Vector2(0, Mathf.Sign(movement.y));

            if (IsBlocked(dir, collisionMask))
                movement.y = 0;
        }

        return movement.normalized;
    }

    private bool IsBlocked(Vector2 direction, LayerMask mask)
    {
        return Physics2D.BoxCast(
            box.bounds.center,
            box.bounds.size,
            0f,
            direction,
            0.05f,
            mask
        ).collider != null;
    }
}
