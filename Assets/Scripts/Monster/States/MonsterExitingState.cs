using UnityEngine;

public class MonsterExitingState : MonsterState
{
    public MonsterExitingState(Monster monster) : base(monster) { }

    public override void Update()
    {
        monster.Movement.MoveTowards(monster.ExitPoint.position);

        if (Vector2.Distance(monster.transform.position, monster.ExitPoint.position) < 0.1f)
        {
            monster.Movement.Stop();
            UnityEngine.Object.Destroy(monster.gameObject);
        }
    }
}
