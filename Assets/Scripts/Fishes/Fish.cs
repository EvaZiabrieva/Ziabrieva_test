using UnityEngine;

public class Fish : MonoBehaviour, IHookAttachable
{
    private FishData _fishData;
    private BaseFishView _view;
    private BaseFishBehaviour _behaviour;
    private BaseFishBehaviourController _controller;
    private FishVisualsContainer _container;
    private FishingProgressSystem _progressSystem;
    public FishData Data => _fishData;
    public BaseFishBehaviour Behaviour => _behaviour;
    public BaseFishBehaviourController Controller => _controller;
    public FishVisualsContainer VisualsContainer => _container;

    public GameObject Visuals => _view.GetFishVisuals();

    public bool ReadyToAttach => true;

    public virtual void Initialize(FishData data, BaseFishView view, 
                       BaseFishBehaviour behaviour, BaseFishBehaviourController controller)
    {
        _fishData = data;
        _view = view;
        _behaviour = behaviour;
        _controller = controller;

        _progressSystem = SystemsContainer.GetSystem<FishingProgressSystem>();
    }
    private void ToggleVisuals(bool isSuccessful)
    {
        if (isSuccessful)
            Visuals.SetActive(true);
        else
        {
            Destroy(Visuals);
        }
    }
    public void Reattach(Transform bucket)
    {
        if (Visuals != null)
        {
            Visuals.transform.parent = bucket;
            Visuals.transform.position = bucket.position;
            Visuals.AddComponent<Rigidbody>();
        }
        
        Destroy(gameObject);
    }

    public void OnAttach() 
    {
        _progressSystem.OnFishingFinished += ToggleVisuals;
    }
}
