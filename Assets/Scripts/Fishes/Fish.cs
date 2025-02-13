using UnityEngine;

public class Fish : MonoBehaviour
{
    private FishData _fishData;
    private BaseFishView _view;
    private BaseFishBehaviour _behaviour;
    private BaseFishBehaviourController _controller;
    private FishVisualsContainer _container;
    public FishData Data => _fishData;
    public BaseFishBehaviour Behaviour => _behaviour;
    public FishVisualsContainer VisualsContainer => _container;

    public virtual void Initialize(FishData data, BaseFishView view, 
                       BaseFishBehaviour behaviour, BaseFishBehaviourController controller)
    {
        _fishData = data;
        _view = view;
        _behaviour = behaviour;
        _controller = controller;
    }

    public void OnBit()
    {
        _view.SetWaterVisualsState(true);
        _controller.Initialize();
    }

    public void OnRelease()
    {
        _view.SetWaterVisualsState(false);
        _controller.Shutdown();
    }
}
