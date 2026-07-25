using UnityEngine;

public class MonsterChasingState : MonsterState
{
    public MonsterChasingState(Monster monster) : base(monster) { }

    public override void Enter()
    {
        Debug.Log("Enter Chasing");
    }

    public override void Update()
    {
        monster.MoveTowards(Player.Instance.transform.position);
    }

    public override void Exit()
    {
        Debug.Log("Exit Chasing");
        monster.StopMoving();
    }
}
