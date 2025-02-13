using System;
using UnityEngine;

public class CastingTracker : IUpdatable
{
    private Transform _trackerTransform;
    private Vector3 _trackedPosition;
    private float _fixedTimeStep;
    private float _timer;
    private UpdatableSystem _updatableSystem;
    public float TrackedDistance => Vector3.Distance(_trackerTransform.position, _trackedPosition);
    public Vector3 TrackedDirection => (_trackedPosition - _trackerTransform.position).normalized;
    public CastingTracker(Transform tracker)
    {
        _trackerTransform = tracker;

        //TODO: get value from configs
        _fixedTimeStep = 0.1f;
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
    }

    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _fixedTimeStep)
        {
            _timer = 0;
            _trackedPosition = _trackerTransform.position;
        }
    }

    public void OnBeforeCast()
    {
        _trackedPosition = _trackerTransform.position;
        _updatableSystem.RegisterUpdatable(this);
    }

    public void OnAfterCast()
    {
        _trackedPosition = _trackerTransform.position;
        _updatableSystem.UnRegisterUpdatable(this);
    }
}
