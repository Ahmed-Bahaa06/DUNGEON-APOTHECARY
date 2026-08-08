using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform exitPoint;
        
    public Transform ExitPoint => exitPoint;

    public Vector3 WaitingPoint { get; private set; }
    public Vector3 EntrancePoint { get; private set; }

    public int Side { get; private set; }

    public MonsterMovement Movement { get; private set; }
    public MonsterRecipe Recipe { get; private set; }

    public MonsterWaitingState WaitingState { get; private set; }
    public MonsterAngryState AngryState { get; private set; }
    public MonsterChasingState ChasingState { get; private set; }
    public MonsterCalmingState CalmingState { get; private set; }
    public MonsterExitingState ExitingState { get; private set; }

    private MonsterState currentState;

    public static event Action OnMonsterServed;
    public event Action OnHealed;

    public Monster Prefab { get; private set; }

    public void SetPrefab(Monster prefab)
    {
        Prefab = prefab;
    }

    private void Awake()
    {
        Movement = GetComponent<MonsterMovement>();
        Recipe = GetComponent<MonsterRecipe>();

        WaitingState = new MonsterWaitingState(this);
        AngryState = new MonsterAngryState(this);
        ChasingState = new MonsterChasingState(this);
        CalmingState = new MonsterCalmingState(this);
        ExitingState = new MonsterExitingState(this);
    }

    private void OnEnable()
    {
        DeliveryManager.Instance.OnCorrectDelivery += DeliverySucceeded;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        currentState?.Update();
    }

    public void Initialize(Vector3 waitingPoint, Vector3 entrancePoint, int side)
    {
        WaitingPoint = waitingPoint;
        EntrancePoint = entrancePoint;
        Side = side;
    }

    public void SetWaitingPoint(Vector3 waitingPoint)
    {
        WaitingPoint = waitingPoint;
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

    public void Served()
    {
        OnMonsterServed?.Invoke();
    }

    private void DeliverySucceeded(Monster monster)
    {
        if (monster != this)
            return;

        Heal();
    }

    public bool CanReceiveDelivery => currentState.CanReceiveDelivery;
    
    private void OnDisable()
    {
        DeliveryManager.Instance.OnCorrectDelivery -= DeliverySucceeded;
    }
}
