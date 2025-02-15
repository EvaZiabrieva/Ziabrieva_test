using UnityEngine;

public class FishBehaviourController : BaseFishBehaviourController
{
    private UpdatableSystem _updatableSystem;
    private FishInteractionSystem _fishInteractionSystem;
    private FishBehaviourData _data;
    private float _delay;
    private float _timer;
    private Vector3 _direciton;
    private Transform _target;
    private LayerMask _layerMask;
    public FishBehaviourController(Fish fish, Transform target) : base(fish) 
    {
        _target = target;

        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
        _fishInteractionSystem =SystemsContainer.GetSystem<FishInteractionSystem>();

        _updatableSystem.RegisterUpdatable(this);
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
        if(Vector3.Distance(_fish.transform.position, _target.position) >= 0.1)
        {
            MoveToTarget();
            return;
        }
        _timer += Time.deltaTime;
        if(Physics.Raycast(_fish.transform.position, _direciton, 1, _layerMask))
        {
            _timer = _delay;
        }

        _fish.Behaviour.Pull(_direciton);

        if (_timer < _delay)
            return;

        _direciton += GetRandomDirection(_data.XDirectionRange, _data.ZDirectionRange);
        _delay = _data.ChangeDirectionDelayRange.random;
        _timer = 0;
    }

    private Vector3 GetRandomDirection(RangeFloat xRange, RangeFloat zRange) => new Vector3(xRange.random, 0, zRange.random);
}
