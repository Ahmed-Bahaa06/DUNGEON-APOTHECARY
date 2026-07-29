using UnityEngine;

public class MonsterWaitingState : MonsterState
{
    public MonsterWaitingState(Monster monster) : base(monster) { }
    
    private float timer;

    public override void Enter()
    {
        timer = GameManager.Instance.MonsterPatience;
    }

    public override void Update()
    {
        monster.Movement.MoveTowards(monster.WaitingPoint);

        bool atWaitingPoint = Vector2.Distance(monster.transform.position, monster.WaitingPoint) < 0.1f;

        if (!atWaitingPoint) return;

        monster.Movement.Stop();

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (SpawnManager.Instance.CanStartChasing())
            {
                monster.ChangeState(monster.AngryState);
            }
        }
    }

    public override void Exit()
    {
        SpawnManager.Instance.MonsterLeftWaiting(monster);
        monster.Movement.Stop();
    }

    public override bool CanReceiveDelivery => true;
}
