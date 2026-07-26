using UnityEngine;

public class MonsterAngryState : MonsterState
{
    private float timer;

    public MonsterAngryState(Monster monster) : base(monster) { }

    public override bool CanReceiveDelivery => true;

    public override void Enter()
    {
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
}
