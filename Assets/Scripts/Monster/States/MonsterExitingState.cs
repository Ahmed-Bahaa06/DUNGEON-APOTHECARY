using UnityEngine;

public class MonsterExitingState : MonsterState
{
    public MonsterExitingState(Monster monster) : base(monster) { }

    public override void Update()
    {
        monster.Movement.MoveTowards(monster.ExitPoint.position);

        if (monster.Movement.ReachedDestination)
        {
            monster.Movement.Stop();
            Object.Destroy(monster.gameObject);
        }
    }
}
