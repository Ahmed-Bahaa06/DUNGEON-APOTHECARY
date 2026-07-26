using UnityEngine;

public class MonsterWaitingState : MonsterState
{
    private float timer;
    public MonsterWaitingState(Monster monster) : base(monster) { }

    public override bool CanReceiveDelivery => true;

    public override void Enter()
    {
        timer = monster.PatienceTime;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            monster.ChangeState(monster.AngryState);
        }
    }
}
