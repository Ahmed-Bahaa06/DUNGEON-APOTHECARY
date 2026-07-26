using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    public Rigidbody2D rb;

    public Vector2 MoveDirection { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        MoveDirection = direction;
        rb.linearVelocity = direction * moveSpeed;
    }

    public void Stop()
    {
        MoveDirection = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
    }

    public void MoveTowards(Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        Move(direction);
    }
}
