using System;
using UnityEngine;

public abstract class BaseBobber
{
    public abstract event Action OnWaterDetected;
    protected BaseBobberView _view;

    protected BaseBobber(BaseBobberView view)
    {
        _view = view;
    }

    public abstract void Initialize();
    public abstract void Shutdown();
    public abstract void UpdateOffset(float reeledDistance);
    public abstract void Cast(Vector3 direction, float force);
}
