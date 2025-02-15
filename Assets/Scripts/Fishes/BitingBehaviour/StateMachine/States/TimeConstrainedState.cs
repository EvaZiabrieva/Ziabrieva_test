using UnityEngine;

public abstract class TimeConstrainedState : BaseState
{
    public override bool Finished
    {
        get => _timer >= _executionTime;
        protected set
        {
            if (value)
                _timer = _executionTime;
        }
    }

    protected float _executionTime;
    protected float _timer;

    protected TimeConstrainedState(FishBehaviourStateMachine stateMachine, float executionTime) : base(stateMachine)
    {
        _executionTime = executionTime;
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        UpdateInternal();
    }

    public override void StartInternal()
    {
        _timer = 0f;
    }

    public abstract void UpdateInternal();
}