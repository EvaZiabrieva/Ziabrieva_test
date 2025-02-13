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

    private float _waterHeight;

    public BobberView(BobberVisualsContainer bobberVisualsContainer, Rigidbody rigidbody) : base(bobberVisualsContainer, rigidbody) {}

    public void FixedUpdate()
    {
        if (Rigidbody.transform.position.y < _waterHeight)
        {
            float displacementMultiplier = Mathf.Clamp01(-Rigidbody.transform.position.y / SUBMERGED_DEPTH) * DISPLACEMENT_AMOUNT;
            Rigidbody.AddForce(new Vector3(0, Mathf.Abs(Physics.gravity.y) *  displacementMultiplier, 0));
        }
    }

    public override void OnAfterBit()
    {
        
    }

    public override void OnBeforeBit()
    {
       
    }

    public override void OnWaterDetected()
    {
        _waterHeight = Rigidbody.transform.position.y - 0.5f;
        Rigidbody.drag = INWATER_DRAG;
        Rigidbody.angularDrag = WATER_ANGULAR_DRAG;

        SystemsContainer.GetSystem<UpdatableSystem>().RegisterFixedUpdatable(this);
    }
}
