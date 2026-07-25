using UnityEngine;

public class MonsterAngryState : MonsterState
{
    private float timer;

    public MonsterAngryState(Monster monster) : base(monster) { }

    public override void Enter()
    {
        Debug.Log("Enter Angry");
        timer = 1f;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            monster.ChangeState(monster.ChasingState);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Angry");
    }
}
