using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookView : BaseHookView
{
    public HookView(HookVisualsContainer hookVisualsContainer) : base(hookVisualsContainer) { }
    public override void Initialize(Vector3 spawnPosition)
    {
        _hookVisualsContainer = GameObject.Instantiate(_hookVisualsContainer, spawnPosition, Quaternion.identity);
    }

    public override void Attach(IHookAttachable attachable)
    {
        _hookAttachable = attachable;
        _attachableVisuals = PoolsContainer.GetFromPool(attachable.Visuals);
        _attachableVisuals.transform.position = _hookVisualsContainer.SnapPlacement.position;
    }

    public override void RemoveAttachment()
    {
        if (_hookAttachable == null)
            return;

        PoolsContainer.ReturnToPool(_hookAttachable.Visuals, _attachableVisuals);
        _hookAttachable = null;
        _attachableVisuals = null;
    }
}
