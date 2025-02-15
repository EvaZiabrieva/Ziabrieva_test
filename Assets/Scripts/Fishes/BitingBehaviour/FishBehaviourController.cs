using UnityEngine;

public class FishBehaviourController : BaseFishBehaviourController
{
    private UpdatableSystem _updatableSystem;
    private FishInteractionSystem _fishInteractionSystem;
    private FishBehaviourData _data;
    private FishBehaviourStateMachine _stateMachine;
    private float _delay;
    private float _timer;
    private Vector3 _direciton;
    private Transform _target;
    private LayerMask _layerMask;

    public FishBehaviourController(Fish fish, Transform target) : base(fish) 
    {
        _target = target;

        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
        _fishInteractionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();
        InitializeStateMachine();

        _updatableSystem.RegisterUpdatable(this);
    }

    private void InitializeStateMachine()
    {
        FollowTargetState followState = new FollowTargetState(_fish.transform, _target);
        _stateMachine = new FishBehaviourStateMachine(_fish.Behaviour, followState);
    }

    public void MoveToTarget()
    {
        Vector3 direction = (_target.position - _fish.transform.position).normalized;
        _fish.transform.position += direction * Time.deltaTime;

        if(Vector3.Distance(_fish.transform.position, _target.position) <= 0.1)
        {
            _updatableSystem.UnRegisterUpdatable(this);
            _fishInteractionSystem.StartBite();
        }
    }

    //TODO: get LayerMask from config
    public override void Initialize(LayerMask _obsticlesLayerMask)
    {
        _data = _fish.Data.BehaviourData;

        RangeFloat delayRange = _data.ChangeDirectionDelayRange;
        _delay = delayRange.random;

        RangeFloat xRange = _data.XDirectionRange;
        RangeFloat zRange = _data.ZDirectionRange;
        _direciton = GetRandomDirection(xRange, zRange);

        _timer = 0;
        _updatableSystem.RegisterUpdatable(this);
        _layerMask = _obsticlesLayerMask;
    }

    public override void Shutdown()
    {
        _updatableSystem.UnRegisterUpdatable(this);
    }

    public override void ExecuteUpdate()
    {
       _stateMachine.Update();
    }

    private Vector3 GetRandomDirection(RangeFloat xRange, RangeFloat zRange) => new Vector3(xRange.random, 0, zRange.random);
}
