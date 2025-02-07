using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : BaseHook
{
    private ConfigurableJoint _joint;
    private SoftJointLimit currentLimit; 
    public Hook(float baitCapacity, float failChance, BaseHookView hookView, ConfigurableJoint joint)
    {
        _baitCapacity = baitCapacity;
        _failChance = failChance;
        _view = hookView;
        _joint = joint;
    }

    public override void SetDistance(float reeledDistance)
    {
        currentLimit = new SoftJointLimit();
        currentLimit.limit = reeledDistance;
        _joint.linearLimit = currentLimit;
    }
}
