using System;
using UnityEngine;

public class MonsterCalmingState : MonsterState
{
    private float timer;

    public MonsterCalmingState(Monster monster) : base(monster) { }

    public override void Enter()
    {
        monster.Movement.Stop();

        timer = 1.5f;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            monster.ChangeState(monster.ExitingState);
        }
    }
}
