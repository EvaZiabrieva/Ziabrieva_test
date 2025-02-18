using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FishingPole : MonoBehaviour, IGrabable
{
    [SerializeField] private float _castingSensitivity;
    [SerializeField] private Transform _poleTip;

    private BaseHook _hook;
    private BaseBobber _bobber;
    private BaseFishingLine _fishingLine;
    private BaseFishingReel _fishingReel;
    private BasePole _pole;

    private UpdatableSystem _updatableSystem;
    private FishingProgressSystem _progressSystem;
    private BaseFishingPoleController _fishingPoleController;
    private BaseFishingPoleBaitingController _baitingController;

    public float CastingSensitivity => _castingSensitivity;
    public BaseHook Hook => _hook;
    public BaseBobber Bobber => _bobber;
    public BaseFishingLine FishingLine => _fishingLine;
    public BaseFishingReel FishingReel => _fishingReel;
    public BasePole Pole => _pole;
    public Transform PoleTip => _poleTip;

    public void Initialize(BaseHook hook, BaseBobber bobber, BaseFishingLine fishingLine,
                                              BaseFishingReel fishingReel, BasePole pole,
                                              BaseFishingPoleController fishingPoleController,
                                              BaseFishingPoleBaitingController baitingController)
    {
        _hook = hook;
        _bobber = bobber;
        _fishingLine = fishingLine;
        _fishingReel = fishingReel;
        _pole = pole;
        _fishingPoleController = fishingPoleController;
        _baitingController = baitingController;

        _hook.AttachDetector.isActive = false;
        _fishingReel.SetRange(0, 360 * (_fishingLine.View.MaxLength / _fishingReel.Data.RoundLength));
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();

        _progressSystem = SystemsContainer.GetSystem<FishingProgressSystem>();
        _progressSystem.OnFishingFinished += SetFinishedView;

        SystemsContainer.GetSystem<FishingProgressSystem>().RegisterFishingPole(this);
    }

    public void OnGrab()
    {
        _updatableSystem.RegisterUpdatable(_fishingPoleController);
        _baitingController.Initialize();
        _hook.Initialize();
        _bobber.Initialize();
        _bobber.OnWaterEntered += (height) => _hook.OnWaterDetectedHandler();
    }
    public void OnDrop()
    {
        _updatableSystem.UnRegisterUpdatable(_fishingPoleController);
        _baitingController.Shutdown();
        _hook.Shutdown();
        _bobber.Shutdown();
        _bobber.OnWaterEntered -= (height) => _hook.OnWaterDetectedHandler();
    }

    private void SetFinishedView(bool isSuccessful)
    {
        _fishingReel.RevertTension();
        _fishingReel.SetAngle(0);

        _bobber.View.Rigidbody.transform.parent = null;
    }
}
