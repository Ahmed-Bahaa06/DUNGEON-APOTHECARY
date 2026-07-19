using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance {  get; private set; }

    private LayerMask wallMask;
    private Rigidbody2D rb;
    private BoxCollider2D box;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        wallMask = LayerMask.GetMask("Wall");
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

        float distance = 0.05f;

        // Check X movement
        if (movement.x != 0)
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                box.bounds.center,
                box.bounds.size,
                0f,
                new Vector2(Mathf.Sign(movement.x), 0),
                distance,
                wallMask);

            if (hit.collider != null)
                movement.x = 0;
        }

        // Check Y movement
        if (movement.y != 0)
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                box.bounds.center,
                box.bounds.size,
                0f,
                new Vector2(0, Mathf.Sign(movement.y)),
                distance,
                wallMask);

            if (hit.collider != null)
                movement.y = 0;
        }

        return movement.normalized;
    }
}
