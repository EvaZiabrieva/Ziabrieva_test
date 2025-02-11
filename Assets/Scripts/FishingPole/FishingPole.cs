using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FishingPole : MonoBehaviour, IGrabable
{
    [SerializeField] private float _castingSensitivity;
    private BaseHook _hook;
    private BaseBobber _bobber;
    private BaseFishingLine _fishingLine;
    private BaseFishingReel _fishingReel;
    private BasePole _pole;
    private GrabableSystem _grabableSystem;
    public float CastingSensitivity => _castingSensitivity;

    private BaseFishingPoleController _fishingPoleController;
    public BaseHook Hook => _hook;
    public BaseBobber Bobber => _bobber;
    public BaseFishingLine FishingLine => _fishingLine;
    public BaseFishingReel FishingReel =>  _fishingReel;
    public BasePole Pole => _pole;

    public event Action OnGrabAction;
    public event Action OnDropAction;

    public void Initialize(BaseHook hook, BaseBobber bobber, BaseFishingLine fishingLine,
                                              BaseFishingReel fishingReel, BasePole pole,
                                              BaseFishingPoleController fishingPoleController)
    {
        _hook = hook;
        _bobber = bobber;
        _fishingLine = fishingLine;
        _fishingReel = fishingReel;
        _pole = pole;
        _fishingPoleController = fishingPoleController;

        _hook.AttachDetector.enabled = false;
        _fishingReel.SetRange(0, 360 * (_fishingLine.View.MaxLength / _fishingReel.RoundLenght));

        _grabableSystem = SystemsContainer.GetSystem<GrabableSystem>();
    }

    public void OnGrab()
    {
        OnGrabAction?.Invoke();
        SystemsContainer.GetSystem<UpdatableSystem>().RegisterUpdatable(_fishingPoleController);

        _grabableSystem.OnAttachableGrab += _hook.CheckForAttach;
        _grabableSystem.OnAttachableDrop += _hook.SetDefault;
    }
    public void OnDrop()
    {
        OnDropAction?.Invoke();
        SystemsContainer.GetSystem<UpdatableSystem>().UnRegisterUpdatable(_fishingPoleController);

        _grabableSystem.OnAttachableGrab -= _hook.CheckForAttach;
        _grabableSystem.OnAttachableDrop -= _hook.SetDefault;
    }
}
