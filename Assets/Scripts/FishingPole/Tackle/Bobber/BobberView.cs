using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BobberView : BaseBobberView, IFixedUpdatable
{
    //TODO: move to configs
    private const float SUBMERGED_DEPTH = 1f;
    private const float DISPLACEMENT_AMOUNT = 2f;
    private const float INWATER_DRAG = 3f;
    private const float WATER_ANGULAR_DRAG = 0f;
    private const float WAVING_MULTILIER = 10f;
    private const float BITING_STRENGTH = 3f;

    private float _waterHeight;
    private float _waterResistanceMultiplier;
    private UpdatableSystem _updatableSystem;
    private FishInteractionSystem _fishInteractionSystem;

    public BobberView(BobberVisualsContainer bobberVisualsContainer, Rigidbody rigidbody) : base(bobberVisualsContainer, rigidbody)
    {
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
        _fishInteractionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();
    }

    public override void Initialize()
    {
        _fishInteractionSystem.OnFishBit += OnFishBitHandler;
        _fishInteractionSystem.OnFishBitTheBait += OnFishBitTheBaitHandler;
        _fishInteractionSystem.OnFishingFinished += OnFishingFinishedHandler;
    }

    public override void Shutdown()
    {
        _fishInteractionSystem.OnFishBit -= OnFishBitHandler;
        _fishInteractionSystem.OnFishBitTheBait -= OnFishBitTheBaitHandler;
        _fishInteractionSystem.OnFishingFinished -= OnFishingFinishedHandler;
    }

    public void ExecuteFixedUpdate()
    {
        if (Rigidbody.transform.position.y < _waterHeight)
        {
            float displacementMultiplier = Mathf.Clamp01(-Rigidbody.transform.position.y / SUBMERGED_DEPTH) * DISPLACEMENT_AMOUNT;
            Vector3 verticalForce = new Vector3(0, Mathf.Abs(Physics.gravity.y) * Mathf.Clamp01(_waterResistanceMultiplier) * displacementMultiplier, 0);
            Rigidbody.AddForce(verticalForce + (Rigidbody.transform.forward * Time.deltaTime * WAVING_MULTILIER));
            _waterResistanceMultiplier += Time.deltaTime;
            return;
        }

        _waterResistanceMultiplier = 0.5f;
    }

    protected override void OnFishBitHandler(Fish fish)
    {
        _waterHeight -= 1;
        Rigidbody.transform.parent = fish.transform;
    }

    protected override void OnFishingFinishedHandler(bool result)
    {
        Rigidbody.transform.parent = null;
    }

    protected override void OnFishBitTheBaitHandler(float strength)
    {
        Rigidbody.AddForce(-Vector3.up * strength * BITING_STRENGTH, ForceMode.Impulse);
    }

    public override void OnWaterEnter(float height)
    {
        Rigidbody.transform.rotation = Quaternion.identity;
        Rigidbody.freezeRotation = true;

        _waterHeight = height;
        Rigidbody.drag = INWATER_DRAG;
        Rigidbody.angularDrag = WATER_ANGULAR_DRAG;
        _waterResistanceMultiplier = INWATER_DRAG;

        _updatableSystem.RegisterFixedUpdatable(this);
    }

    public override void OnWaterExit()
    {
        Rigidbody.freezeRotation = false;

        _waterHeight = 0;
        Rigidbody.drag = 0;
        Rigidbody.angularDrag = 0;

        _updatableSystem.UnRegisterFixedUpdatable(this);
    }
}
