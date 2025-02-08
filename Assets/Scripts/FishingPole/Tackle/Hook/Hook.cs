using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : BaseHook
{
    private ConfigurableJoint _joint;
    private SoftJointLimit currentLimit;
    private Rigidbody _rigidbody;
    private bool isCasted = false;
    private float _castingTimer;

    public Hook(float baitCapacity, float failChance, BaseHookView hookView, ConfigurableJoint joint, Rigidbody rigidbody)
    {
        //TODO: get values from configs
        _baitCapacity = baitCapacity;
        _failChance = failChance;
        _view = hookView;
        _joint = joint;
        _rigidbody = rigidbody;
    }

    public override void Cast(Vector3 direction, float force)
    {
        _castingTimer += Time.deltaTime;
        _rigidbody.isKinematic = true;
        _joint.xMotion = ConfigurableJointMotion.Free;
        _joint.yMotion = ConfigurableJointMotion.Free;
        _joint.zMotion = ConfigurableJointMotion.Free;

        _rigidbody.transform.position += (direction * force) * Time.deltaTime + Physics.gravity * _castingTimer * _castingTimer / 2f;

        //CastLogic(direction, force);
    }
    private void CastLogic(Vector3 direction, float force)
    {

        //if (!isCasted)
        //{
        //    isCasted = true;
        //    _rigidbody.velocity = Vector3.zero;
        //
        //    _joint.xMotion = ConfigurableJointMotion.Free;
        //    _joint.yMotion = ConfigurableJointMotion.Free;
        //    _joint.zMotion = ConfigurableJointMotion.Free;
        //
        //    _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        //}
    }

    public override void UpdateOffset(float reeledDistance)
    {
        currentLimit = new SoftJointLimit();
        currentLimit.limit = reeledDistance;
        _joint.linearLimit = currentLimit;
    }
}
