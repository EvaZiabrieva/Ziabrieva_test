using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class FishingReel : BaseFishingReel
{
    private XRKnob _knob;
    private RangeFloat _knobRange;
    public FishingReel(BaseFishingReelView fishingReelView, XRKnob knob, float roundLenght)
    {
        _view = fishingReelView;
        _knob = knob;
        _roundLenght = roundLenght;
    }

    public override void ApplyTension(float tension)
    {
        _knob.maxAngle = _knobRange.max * tension;
        _knob.value /= tension;
    }

    public override float GetLength() => Mathf.Lerp(_knob.minAngle, _knob.maxAngle, _knob.value) / 360 * _roundLenght;

    public override float SetAngle(float angle) => _knob.value = angle;

    public override void SetRange(float minAngle, float maxAngle)
    {
        _knobRange.min = minAngle;
        _knobRange.max = maxAngle;

        _knob.minAngle = _knobRange.min;
        _knob.maxAngle = _knobRange.max;
    }
}
