using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    private Rigidbody2D rb;
    private BoxCollider2D box;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    public void Move(Vector2 moveInput, float moveSpeed)
    {
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
