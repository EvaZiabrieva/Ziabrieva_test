/// <summary>
/// Possibility to extned fish with unique data or logic
/// </summary>
public abstract class BaseFish
{
    protected FishData _fishData;
    protected BaseFishView _view;
    protected BaseFishBehaviour _behaviour;
    protected BaseFishBehaviourController _controller;
    public FishData Data => _fishData;
    public BaseFishBehaviour Behaviour => _behaviour;

    protected BaseFish(FishData data, BaseFishView view)
    {
        _fishData = data;
        _view = view;
    }

    public abstract void OnBit();
}
