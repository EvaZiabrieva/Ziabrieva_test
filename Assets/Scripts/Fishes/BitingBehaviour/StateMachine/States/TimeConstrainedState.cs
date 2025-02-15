using UnityEngine;

public abstract class TimeConstrainedState : BaseState
{
    protected float _executionTime;
    protected float _timer;

    protected TimeConstrainedState(BehaviourStateMachine stateMachine, float executionTime) : base(stateMachine)
    {
        _executionTime = executionTime;
    }

    public override void Start()
    {
        _timer = 0f;
        StartInternal();
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _executionTime)
        {
            Stop();
        }

        UpdateInternal();
    }

    public abstract void StartInternal();
    public abstract void UpdateInternal();
}
