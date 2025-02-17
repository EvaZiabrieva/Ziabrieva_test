using UnityEngine;

public abstract class BaseFishingPoleBaitingController :  IUpdatable
{
    protected FishingPole _fishingPole;
    protected Fish _baitedFish;

    protected BaseFishingPoleBaitingController(FishingPole pole)
    {
        _fishingPole = pole;
    }
    public abstract void Initialize();
    public abstract void Shutdown();
    public abstract void ExecuteUpdate();
}
