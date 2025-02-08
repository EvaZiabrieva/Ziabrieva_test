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
    public override float GetAngle() => _knob.value * _roundLenght;

    public override void ReelOnCast(Vector3 direction, float force)
    {
        
    }
}
