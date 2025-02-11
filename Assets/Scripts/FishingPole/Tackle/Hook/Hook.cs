using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : BaseHook
{
    private ConfigurableJoint _joint;
    private SoftJointLimit currentLimit;
    private Rigidbody _rigidbody;

    public Hook(float baitCapacity, float failChance, BaseHookView hookView, ConfigurableJoint joint, Rigidbody rigidbody, AttachDetector attachDetector)
    {
        //TODO: get values from configs
        _baitCapacity = baitCapacity;
        _failChance = failChance;
        _view = hookView;
        _joint = joint;
        _rigidbody = rigidbody;
        _attachDetector = attachDetector;
        _currentBaitCapacity = 0;
    }

    public override void CheckForAttach()
    {
        if (_currentBaitCapacity < _baitCapacity)
        {
            _attachDetector.enabled = true;
            _attachDetector.OnAttach += _view.Attach;
            _attachDetector.OnAttach += ChangeCapacity;

            _view.SetReadyToAttachVisual();
        }
        else
        {
            if(_attachDetector.enabled)
            {
                _attachDetector.enabled = false;
                _attachDetector.OnAttach -= _view.Attach;
                _attachDetector.OnAttach -= ChangeCapacity;
            }

            _view.SetNotReadyToAttachVisual();
        }
    }

    public override void SetDefault()
    {
        _attachDetector.enabled = false;
        _attachDetector.OnAttach -= _view.Attach;
        _view.SetDefaultVisual();
    }
    private void ChangeCapacity(IHookAttachable attachable)
    {
        _currentBaitCapacity++;
    }
}
