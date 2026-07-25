using UnityEngine;

public class MonsterExitingState : MonsterState
{
    public MonsterExitingState(Monster monster) : base(monster) { }

    public override void Enter()
    {
        Debug.Log("Enter Exiting");
    }

    public override void Update()
    {
        monster.MoveTowards(monster.ExitPoint.position);

        if (Vector2.Distance(monster.transform.position, monster.ExitPoint.position) < 0.1f)
        {
            monster.StopMoving();
            UnityEngine.Object.Destroy(monster.gameObject);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Exiting");

    }
}
