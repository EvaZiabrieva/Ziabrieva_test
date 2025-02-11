using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : BaseBobber
{
    private ConfigurableJoint _joint;
    private SoftJointLimit currentLimit;
    private Rigidbody _rigidbody;
    private FishInteractionSystem _fishInteractionSystem;

    public Bobber(BaseBobberView baseBobberView, ConfigurableJoint joint, Rigidbody rigidbody)
    {
        _view = baseBobberView;
        _joint = joint;
        _rigidbody = rigidbody;
    }

    public override void Initialize()
    {
        _fishInteractionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();
        _fishInteractionSystem.OnFishBit += OnFisBitHandler;
        _fishInteractionSystem.OnFishBitTheBait += OnFishBitTheBaitHandler;
    }

    public override void Shutdown()
    {
        _fishInteractionSystem.OnFishBit -= OnFisBitHandler;
        _fishInteractionSystem.OnFishBitTheBait -= OnFishBitTheBaitHandler;
    }

    public override void Cast(Vector3 direction, float force)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
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

    private void OnFisBitHandler()
    {
        Debug.Log("Fish bit");
    }
}