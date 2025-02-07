using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class FishingReel : BaseFishingReel
{
    private XRKnob _knob;
    public FishingReel(BaseFishingReelView fishingReelView, XRKnob knob)
    {
        _view = fishingReelView;
        _knob = knob;
    }
    public override float GetAngle() => _knob.value;
}
