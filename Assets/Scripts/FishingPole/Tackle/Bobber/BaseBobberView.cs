
using UnityEngine;

public abstract class BaseBobberView
{
    protected BobberVisualsContainer _bobberVisualsContainer;
    public Rigidbody Rigidbody { get; protected set; }
    public BaseBobberView(BobberVisualsContainer bobberVisualsContainer, Rigidbody rigidbody)
    {
        _bobberVisualsContainer = bobberVisualsContainer;
        Rigidbody = rigidbody;
    }
    public abstract void Initialize();
    public abstract void Shutdown();
    public abstract void OnFishBitTheBaitHandler(float strength);
    public abstract void OnFishBitHandler(Fish fish);
    public abstract void OnWaterDetected();
}
