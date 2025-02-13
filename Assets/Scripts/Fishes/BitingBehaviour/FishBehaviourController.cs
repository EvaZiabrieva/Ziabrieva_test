using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviourController : BaseFishBehaviourController
{
    private UpdatableSystem _updatableSystem;
    private FishBehaviourData _data;
    private float _delay;
    private float _timer;
    private Vector3 _direciton;

    public FishBehaviourController(Fish fish) : base(fish) 
    {
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
    }

    public override void Initialize()
    {
        _data = _fish.Data.BehaviourData;

        RangeFloat delayRange = _data.ChangeDirectionDelayRange;
        _delay = GetRandomDelay(delayRange);

        RangeFloat xRange = _data.XDirectionRange;
        RangeFloat zRange = _data.ZDirectionRange;
        _direciton = GetRandomDirection(xRange, zRange);

        _timer = 0;
        _updatableSystem.RegisterUpdatable(this);
    }

    public override void Shutdown()
    {
        _updatableSystem.UnRegisterUpdatable(this);
    }

    public override void ExecuteUpdate()
    {
        _timer += Time.deltaTime;

        _fish.Behaviour.Pull(_direciton);

        if (_timer < _delay)
            return;

        _direciton += GetRandomDirection(_data.XDirectionRange, _data.ZDirectionRange);
        _delay = GetRandomDelay(_data.ChangeDirectionDelayRange);
        _timer = 0;
    }

    private float GetRandomDelay(RangeFloat delayRange) => Random.Range(delayRange.min, delayRange.max);

    private Vector3 GetRandomDirection(RangeFloat xRange, RangeFloat zRange)
    {
        float x = Random.Range(xRange.min, xRange.max);
        float z = Random.Range(zRange.min, zRange.max);
        return new Vector3(x, 0, z);
    }
}
