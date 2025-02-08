using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPoleController : BaseFishingPoleController
{
    private bool _isCasting = false;
    private Vector3 _castingDirection;
    private float _castingSpeed;
    public FishingPoleController(FishingPole pole, CastingTracker tracker) : base(pole, tracker) 
    {
        pole.OnGrabAction += tracker.OnGrabHandler;
        pole.OnDropAction += tracker.OnDropHandler;
    }

    public override void Update()
    {  
        Debug.Log($"Tracker distance: {_castingTracker.TrackedDistance}");

        if(_castingTracker.TrackedDistance >= _trackedDistanceTreshold && !_isCasting)
        {
            _isCasting = true;
            _castingDirection = _fishingPole.transform.forward + _fishingPole.transform.up;
            _castingSpeed = _castingTracker.TrackedDistance * 2;
        }

        if(_isCasting)
        {
            _fishingPole.Hook.Cast(_castingDirection, _castingSpeed);
        }
    }

    public void Shutdown()
    {
        _fishingPole.OnGrabAction -= _castingTracker.OnGrabHandler;
        _fishingPole.OnDropAction -= _castingTracker.OnDropHandler;
    }
}
