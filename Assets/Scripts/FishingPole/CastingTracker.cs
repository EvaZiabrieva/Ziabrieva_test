using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingTracker : IUpdatable
{
    private Transform _trackerTransform;
    private Vector3 _trackedPosition;
    private float _fixedTimeStep;
    private float _timer;

    public float TrackedDistance => Vector3.Distance(_trackerTransform.position, _trackedPosition);
    public Vector3 TrackedDirection => (_trackedPosition - _trackerTransform.position).normalized;
    public CastingTracker(Transform tracker)
    {
        _trackerTransform = tracker;

        //TODO: get value from configs
        _fixedTimeStep = 0.1f;
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
        SystemsContainer.GetSystem<UpdatableSystem>().RegisterUpdatable(this);
    }

    public void OnAfterCast()
    {
        _trackedPosition = _trackerTransform.position;
        SystemsContainer.GetSystem<UpdatableSystem>().UnRegisterUpdatable(this);
    }
}
