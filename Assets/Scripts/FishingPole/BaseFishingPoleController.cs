using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseFishingPoleController : IUpdatable
{
    protected readonly FishingPole _fishingPole;
    protected readonly CastingTracker _castingTracker;
    protected readonly float _trackedDistanceTreshold;

    protected BaseFishingPoleController(FishingPole pole, CastingTracker castingTracker)
    {
        _fishingPole = pole;
        _castingTracker = castingTracker;

        //TODO: get value from configs
        _trackedDistanceTreshold = 1;
    }

    public abstract void ExecuteUpdate();
}
