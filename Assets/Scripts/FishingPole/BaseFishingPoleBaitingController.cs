using UnityEngine;

public abstract class BaseFishingPoleBaitingController :  IUpdatable
{
    protected BaseFish _baitedFish;
    public abstract void Initialize();
    public abstract void Shutdown();
    public abstract void Update();
}
