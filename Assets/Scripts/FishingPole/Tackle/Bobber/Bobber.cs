using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : BaseBobber
{
    private ConfigurableJoint _joint;
    private SoftJointLimit currentLimit;
    private FishInteractionSystem _fishInteractionSystem;
    private CollisionByLayerDetector _waterDetector;

    public override event Action OnWaterDetected
    {
        add => _waterDetector.OnWaterDetected += value; 
        remove => _waterDetector.OnWaterDetected -= value; 
    }

    public Bobber(BaseBobberView view, CollisionByLayerDetector waterDetector,
                  ConfigurableJoint joint) : base(view)
    {
        _waterDetector = waterDetector;
        _joint = joint;

        _fishInteractionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();
    }

    public override void Initialize()
    {
        OnWaterDetected += OnWaterDetectedHandler;
        _view.Initialize();
    }

    public override void Shutdown()
    {
        OnWaterDetected -= OnWaterDetectedHandler;
        _view.Shutdown();
    }

    public override void Cast(Vector3 direction, float force)
    {
        _view.Rigidbody.velocity = Vector3.zero;
        _view.Rigidbody.AddForce(direction * force, ForceMode.Impulse);
        _waterDetector.IsActive = true;
    }

    public override void UpdateOffset(float reeledDistance)
    {
        SetLimit(reeledDistance);
    }

    private void SetLimit(float limit)
    {
        currentLimit = new SoftJointLimit();
        currentLimit.limit = limit;
        _joint.linearLimit = currentLimit;
    }
    
    private void OnWaterDetectedHandler()
    {
        _waterDetector.IsActive = false;
        _view.OnWaterDetected();
    }
}