using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberView : BaseBobberView, IFixedUpdatable
{
    //TODO: move to configs
    private const float SUBMERGED_DEPTH = 1f;
    private const float DISPLACEMENT_AMOUNT = 3f;
    private const float INWATER_DRAG = 5f;
    private const float WATER_ANGULAR_DRAG = 5;
    private const float BITING_STRENGTH = 3;

    private float _waterHeight;
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
    }

    public override void Shutdown()
    {
        _fishInteractionSystem.OnFishBit -= OnFishBitHandler;
        _fishInteractionSystem.OnFishBitTheBait -= OnFishBitTheBaitHandler;
    }

    public void ExecuteFixedUpdate()
    {
        if (Rigidbody.transform.position.y < _waterHeight)
        {
            float displacementMultiplier = Mathf.Clamp01(-Rigidbody.transform.position.y / SUBMERGED_DEPTH) * DISPLACEMENT_AMOUNT;
            Rigidbody.AddForce(new Vector3(0, Mathf.Abs(Physics.gravity.y) *  displacementMultiplier, 0));
        }
    }

    public override void OnFishBitHandler(Fish fish)
    {
        _waterHeight -= 1;
        Rigidbody.transform.parent = fish.transform;
    }

    public override void OnFishBitTheBaitHandler(float strength)
    {
        Rigidbody.AddForce(-Vector3.up * strength * BITING_STRENGTH, ForceMode.Impulse);
    }

    public override void OnWaterDetected()
    {
        _bobberVisualsContainer.ParticleSystem.gameObject.SetActive(true);
        _bobberVisualsContainer.ParticleSystem.Play();

        Vector3 rotation = new Vector3(Rigidbody.transform.rotation.x, Rigidbody.transform.rotation.y, 0);
         Rigidbody.transform.rotation = Quaternion.Euler(rotation);
        Rigidbody.freezeRotation = true;

        _waterHeight = Rigidbody.transform.position.y;
        Rigidbody.drag = INWATER_DRAG;
        Rigidbody.angularDrag = WATER_ANGULAR_DRAG;

        _updatableSystem.RegisterFixedUpdatable(this);
    }
}
