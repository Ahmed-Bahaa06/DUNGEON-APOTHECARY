using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField] private float patienceTime = 5f;
    public float PatienceTime => patienceTime;

    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private Transform exitPoint;
    public Transform ExitPoint => exitPoint;

    private Vector2 moveDirection;
    public Vector2 MoveDirection => moveDirection;

    public MonsterWaitingState WaitingState { get; private set; }
    public MonsterAngryState AngryState { get; private set; }
    public MonsterChasingState ChasingState { get; private set; }
    public MonsterCalmingState CalmingState { get; private set; }
    public MonsterExitingState ExitingState { get; private set; }

    [Header("Recipe")]
    [SerializeField] public CraftedCureRecipeSO recipeSO;

    private ItemSO requiredCure;
    private MonsterState currentState;

    public event Action OnHealed;

    public Rigidbody2D Rigidbody { get; private set; }

    private void Awake()
    {
        requiredCure = recipeSO.craftedCure;
        Rigidbody = GetComponent<Rigidbody2D>();

        WaitingState = new MonsterWaitingState(this);
        AngryState = new MonsterAngryState(this);
        ChasingState = new MonsterChasingState(this);
        CalmingState = new MonsterCalmingState(this);
        ExitingState = new MonsterExitingState(this);
    }

    private void Start()
    {
        ChangeState(WaitingState);
    }

    private void Update()
    {
        currentState?.Update();
    }


    public void ChangeState(MonsterState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
    }

    public void Heal()
    {
        OnHealed?.Invoke();
        ChangeState(CalmingState);
    }

    public bool ReceiveCure(ItemSO cure)
    {
        Debug.Log("Expected: " + requiredCure.itemName);
        Debug.Log("Received: " + cure.itemName);

        return cure == requiredCure;
    }

    public void Move(Vector2 direction)
    {
        moveDirection = direction;
        Rigidbody.linearVelocity = direction * moveSpeed;
    }

    public void StopMoving()
    {
        moveDirection = Vector2.zero;
        Rigidbody.linearVelocity = Vector2.zero;
    }

    public void MoveTowards(Vector3 targetPosition)
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        Move(direction);
    }
}
