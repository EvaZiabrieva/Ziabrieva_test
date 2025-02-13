using System;
using System.Collections;
using System.Collections.Generic;
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
    private void OnFishBitHandler(BaseFish fish)
    {
        _baitedFish = fish;
        _updatableSystem.RegisterUpdatable(this);
    }

    public override void Shutdown()
    {
        _fishInteractionSystem.OnFishBit -= OnFishBitHandler;
    }

    public override void Update()
    {
        FishPullingData data = _baitedFish.Behaviour.GetPullingData();
        //calculate fishing progress depend on user input direction and fish pulling data
    }
}
