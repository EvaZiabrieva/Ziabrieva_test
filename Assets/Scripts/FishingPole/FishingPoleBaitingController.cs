using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//TODO: Add abstraction
public class FishingPoleBaitingController : BaseFishingPoleBaitingController
{
    private FishInteractionSystem _fishInteractionSystem;
    private UpdatableSystem _updatableSystem;

    public FishingPoleBaitingController(FishingPole pole) : base(pole) 
    {
        _fishInteractionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
    }

    public override void Initialize()
    {
        _fishInteractionSystem.OnFishBit += OnFishBitHandler;
    }

    //TODO: implement fish released logic
    private void OnFishBitHandler(Fish fish)
    {
        _baitedFish = fish;
        _fishingPole.FishingReel.ApplyTension(_baitedFish.Data.BehaviourData.Strength);
        _updatableSystem.RegisterUpdatable(this);
    }

    public override void Shutdown()
    {
        _fishInteractionSystem.OnFishBit -= OnFishBitHandler;
    }

    public override void ExecuteUpdate()
    {
       
    }
}
