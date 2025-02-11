using UnityEngine;

public abstract class BaseBobber
{
    protected BaseBobberView _view;
    public abstract void UpdateOffset(float reeledDistance);
    public abstract void Cast(Vector3 direction, float force);
}
