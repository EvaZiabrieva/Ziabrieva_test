using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPoleController : BaseFishingPoleController
{
    public FishingPoleController(FishingPole pole) : base(pole) { }
    public override void Update()
    {
        _fishingPole.FishingLine.View.SetLenght(_fishingPole.FishingReel.GetAngle());
        _fishingPole.Hook.SetDistance(_fishingPole.FishingReel.GetAngle());
    }
}
