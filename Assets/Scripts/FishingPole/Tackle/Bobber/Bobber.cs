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
        _fishInteractionSystem.OnFishBit += OnFisBitHandler;
        _fishInteractionSystem.OnFishBitTheBait += OnFishBitTheBaitHandler;
        OnWaterDetected += OnWaterDetectedHandler;
        OnWaterDetected += _view.OnWaterDetected;
    }

    public override void Shutdown()
    {
        _fishInteractionSystem.OnFishBit -= OnFisBitHandler;
        _fishInteractionSystem.OnFishBitTheBait -= OnFishBitTheBaitHandler;
        OnWaterDetected -= OnWaterDetectedHandler;
        OnWaterDetected -= _view.OnWaterDetected;
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

    private void OnFishBitTheBaitHandler()
    {
        Debug.Log("Fish bit the bait");
    }

    private void OnFisBitHandler(BaseFish fish)
    {
        Debug.Log("Fish bit");
    }

    private void OnWaterDetectedHandler()
    {
        Debug.Log("Water detected");
        _waterDetector.IsActive = false;
    }
}