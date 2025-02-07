using UnityEngine;

public abstract class BaseHook
{
    protected float _baitCapacity;
    protected float _failChance;
    protected BaseHookView _view;
    public BaseHookView View => _view;

    public abstract void SetDistance(float reeledDistance);
}


