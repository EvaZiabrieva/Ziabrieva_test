using UnityEngine;

public class FollowTargetState : BaseState
{
    public override bool Finished 
    {
        get => GetDistanceToTarget() <= _desiredDistance;
        protected set 
        { 
            if(value)
                _desiredDistance = GetDistanceToTarget();
        }
    }

    private Transform _fish;
    private Transform _target;
    private float _desiredDistance;

    public FollowTargetState(Transform fish, Transform target)
    {
        _fish = fish;
        _target = target;
        _desiredDistance = 0.1f; //default offset to check we close enough
    }

    public override void Update()
    {
        Vector3 direction = (_target.position - _fish.position).normalized;
        _stateMachine.Behaviour.Pull(direction);
    }

    private float GetDistanceToTarget() => Vector3.Distance(_target.position, _fish.position);
}
