using UnityEngine;

public class MonsterWaitingState : MonsterState
{
    private float timer;
    public MonsterWaitingState(Monster monster) : base(monster) { }

    public override void Enter()
    {
        Debug.Log("Enter Waiting");

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

    public override void Exit()
    {
        Debug.Log("Exit Waiting");
    }
}
