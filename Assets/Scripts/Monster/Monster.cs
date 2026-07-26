using UnityEngine;
using System;

public class Monster : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField] private float patienceTime = 5f;
    public float PatienceTime => patienceTime;

    [SerializeField] private Transform exitPoint;
    public Transform ExitPoint => exitPoint;

    public MonsterMovement Movement { get; private set; }
    public MonsterRecipe Recipe { get; private set; }

    public MonsterWaitingState WaitingState { get; private set; }
    public MonsterAngryState AngryState { get; private set; }
    public MonsterChasingState ChasingState { get; private set; }
    public MonsterCalmingState CalmingState { get; private set; }
    public MonsterExitingState ExitingState { get; private set; }

    private MonsterState currentState;

    public event Action OnHealed;

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

    private void Start()
    {
        ChangeState(WaitingState);
    }

    private void OnEnable()
    {
        DeliveryManager.Instance.OnCorrectDelivery += DeliverySucceeded;
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
