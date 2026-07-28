using UnityEngine;

public class MonsterChasingState : MonsterState
{
    public MonsterChasingState(Monster monster) : base(monster) { }

    public override bool CanReceiveDelivery => true;

    public override void Update()
    {
        monster.Movement.MoveTowards(Player.Instance.transform.position);
    }

    public override void Exit()
    {
        SpawnManager.Instance.MonsterStoppedChasing();
        monster.Movement.Stop();
    }
}
