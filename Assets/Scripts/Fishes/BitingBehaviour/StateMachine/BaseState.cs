using UnityEngine;

public abstract class BaseState 
{
    public abstract bool Finished { get; protected set; }

    protected FishBehaviourStateMachine _stateMachine;
   
    public void Start(FishBehaviourStateMachine stateMachine)
    {
        Debug.Log($"{this} started");
        _stateMachine = stateMachine;
        StartInternal();
    }

    public abstract void Update();

    public void Stop()
    {
        Finished = true;
        _stateMachine = null;
        StopInternal();

        Debug.Log($"{this} stopped");
    }

    public virtual void StartInternal() { }
    public virtual void StopInternal() { }
}
