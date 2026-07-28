using UnityEngine;

public class MonsterAngryState : MonsterState
{
    public MonsterAngryState(Monster monster) : base(monster) { }

    public override bool CanReceiveDelivery => true;

    public override void Update()
    {
        monster.Movement.MoveTowards(monster.EntrancePoint);

        if (Vector2.Distance(monster.transform.position, monster.EntrancePoint) < 0.1f)
        {
            monster.transform.position = monster.EntrancePoint;
            monster.ChangeState(monster.ChasingState);
        }
    }

    public override void Exit()
    {
        monster.Movement.Stop();
    }
}

