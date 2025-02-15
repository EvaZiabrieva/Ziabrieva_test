public abstract class BaseState 
{
    protected BehaviourStateMachine _stateMachine;
    protected BaseState(BehaviourStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public abstract void Start();
    public abstract void Update();
    public abstract void Stop();
}
