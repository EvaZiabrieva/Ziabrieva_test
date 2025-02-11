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

    private UpdatableSystem _updatableSystem;
    private BaseFishingPoleController _fishingPoleController;

    public float CastingSensitivity => _castingSensitivity;
    public BaseHook Hook => _hook;
    public BaseBobber Bobber => _bobber;
    public BaseFishingLine FishingLine => _fishingLine;
    public BaseFishingReel FishingReel =>  _fishingReel;
    public BasePole Pole => _pole;

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
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
    }

    public void OnGrab()
    {
        _updatableSystem.RegisterUpdatable(_fishingPoleController);
        _hook.Initialize();
        _bobber.Initialize();
    }
    public void OnDrop()
    {
        _updatableSystem.UnRegisterUpdatable(_fishingPoleController);
        _hook.Shutdown();
        _bobber.Shutdown();
    }
}
