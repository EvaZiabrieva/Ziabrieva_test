using UnityEngine;

public abstract class BaseFishBehaviourController : IUpdatable
{
    protected Fish _fish;

    protected BaseFishBehaviourController(Fish fish)
    {
        _fish = fish;
    }

    public abstract void Initialize(LayerMask _obsticlesLayerMask);
    public abstract void Shutdown();
    public abstract void ExecuteUpdate();
}
