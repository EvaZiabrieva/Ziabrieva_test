using UnityEngine;

public abstract class BaseHook
{
    protected float _baitCapacity;
    protected float _currentBaitCapacity;
    protected float _failChance;
    protected BaseHookView _view;
    protected AttachDetector _attachDetector;
    public BaseHookView View => _view;
    public AttachDetector AttachDetector => _attachDetector;
    public float BaitCapacity => _baitCapacity;
    public float CurrentBaitCapacity => _currentBaitCapacity;
    public abstract void CheckForAttach();
    public abstract void SetDefault();
}


