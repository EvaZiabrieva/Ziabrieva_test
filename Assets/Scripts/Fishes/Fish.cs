using UnityEngine;

public class Fish : MonoBehaviour, IHookAttachable
{
    private FishData _fishData;
    private BaseFishView _view;
    private BaseFishBehaviour _behaviour;
    private BaseFishBehaviourController _controller;
    private FishVisualsContainer _container;
    public FishData Data => _fishData;
    public BaseFishBehaviour Behaviour => _behaviour;
    public BaseFishBehaviourController Controller => _controller;
    public FishVisualsContainer VisualsContainer => _container;

    public GameObject Visuals => gameObject;

    public bool ReadyToAttach => true;

    public virtual void Initialize(FishData data, BaseFishView view, 
                       BaseFishBehaviour behaviour, BaseFishBehaviourController controller)
    {
        _fishData = data;
        _view = view;
        _behaviour = behaviour;
        _controller = controller;  
    }

    public void OnAttach()
    {
        _view.SetWaterVisualsState(true);
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
