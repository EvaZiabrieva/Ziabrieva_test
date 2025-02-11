using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : BaseHook
{
    private ConfigurableJoint _joint;
    private SoftJointLimit currentLimit;
    private Rigidbody _rigidbody;
    private GrabableSystem _grabableSystem;

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

        _grabableSystem = SystemsContainer.GetSystem<GrabableSystem>();
    }
    public override void Initialize()
    {
        _attachDetector.OnAttach += _view.Attach;
        _attachDetector.OnAttach += OnAttachHandler;

        _grabableSystem.OnAttachableGrab += CheckForAttach;
        _grabableSystem.OnAttachableDrop += SetDefault;
    }

    public override void Shutdown()
    {
        _attachDetector.OnAttach -= _view.Attach;
        _attachDetector.OnAttach -= OnAttachHandler;

        _grabableSystem.OnAttachableGrab -= CheckForAttach;
        _grabableSystem.OnAttachableDrop -= SetDefault;
    }

    public override void CheckForAttach()
    {
        if (_currentBaitCapacity < _baitCapacity)
        {
            _attachDetector.enabled = true;
            _view.SetReadyToAttachVisual();
        }
        else
        {
            _view.SetNotReadyToAttachVisual();
        }
    }

    public override void SetDefault()
    {
        _attachDetector.enabled = false;
        _view.SetDefaultVisual();
    }

    private void OnAttachHandler(IHookAttachable attachable)
    {
        _currentBaitCapacity++;
        _attachDetector.enabled = false;
    }
}
