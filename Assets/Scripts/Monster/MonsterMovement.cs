using UnityEngine;
using Pathfinding;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;

    public Vector2 MoveDirection { get; private set; }

    private void Awake()
    {
        if (aiPath == null)
            aiPath = GetComponent<AIPath>();
    }

    private void Update()
    {
        MoveDirection = aiPath.desiredVelocity.normalized;
    }


    public void Move(Vector2 direction)
    {
        aiPath.isStopped = false;

        aiPath.destination = transform.position + (Vector3)direction;
    }

    public void MoveTowards(Vector3 targetPosition)
    {
        aiPath.isStopped = false;
        aiPath.destination = targetPosition;
    }

    public void Stop()
    {
        aiPath.isStopped = true;
        aiPath.destination = transform.position;
        MoveDirection = Vector2.zero;
    }

    public bool ReachedDestination => aiPath.reachedDestination;
}
