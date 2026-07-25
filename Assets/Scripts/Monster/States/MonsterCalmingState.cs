using System;
using UnityEngine;

public class MonsterCalmingState : MonsterState
{
    private float timer;
    

    public MonsterCalmingState(Monster monster) : base(monster) { }

    public override void Enter()
    {
        Debug.Log("Enter Calming");
        timer = 2f;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            monster.ChangeState(monster.ExitingState);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Calming");

    }
}
