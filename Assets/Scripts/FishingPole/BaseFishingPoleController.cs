using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseFishingPoleController : IUpdatable
{
    protected readonly FishingPole _fishingPole;

    protected BaseFishingPoleController(FishingPole pole)
    {
        _fishingPole = pole;
        SystemsContainer.GetSystem<UpdatableSystem>().RegisterUpdatable(this);
    }

    public abstract void Update();
}
