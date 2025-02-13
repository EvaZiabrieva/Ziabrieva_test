
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
    public abstract void OnBeforeBit();
    public abstract void OnAfterBit();
    public abstract void OnWaterDetected();
}
