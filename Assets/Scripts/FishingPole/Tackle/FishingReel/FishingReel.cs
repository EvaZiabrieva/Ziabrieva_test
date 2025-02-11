using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class FishingReel : BaseFishingReel
{
    private XRKnob _knob;
    public FishingReel(BaseFishingReelView fishingReelView, XRKnob knob, float roundLenght)
    {
        _view = fishingReelView;
        _knob = knob;
        _roundLenght = roundLenght;
    }
    public override float GetAngle() => Mathf.Lerp(_knob.minAngle, _knob.maxAngle, _knob.value);

    public override float SetAngle(float angle) => _knob.value = angle;

    public override void SetRange(float minAngle, float maxAngle)
    {
        _knob.minAngle = minAngle;
        _knob.maxAngle = maxAngle;
    }
}
