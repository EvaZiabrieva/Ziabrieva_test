
public abstract class BaseBobberView
{
    protected BobberVisualsContainer _bobberVisualsContainer;
    public BaseBobberView(BobberVisualsContainer bobberVisualsContainer)
    {
        _bobberVisualsContainer = bobberVisualsContainer;
    }
    public abstract void OnBeforeBit();
    public abstract void OnAfterBit();
}
