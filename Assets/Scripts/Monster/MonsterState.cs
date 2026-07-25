public abstract class MonsterState
{
    protected Monster monster;

    public MonsterState(Monster monster)
    {
        this.monster = monster;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}