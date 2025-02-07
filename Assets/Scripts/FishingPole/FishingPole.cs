using UnityEngine;

public class FishingPole : MonoBehaviour
{
    private BaseHook _hook;
    private BaseBobber _bobber;
    private BaseFishingLine _fishingLine;
    private BaseFishingReel _fishingReel;
    private BasePole _pole;

    private BaseFishingPoleController _fishingPoleController;
    private bool isInitialized = false;
    public BaseHook Hook => _hook;
    public BaseBobber Bobber => _bobber;
    public BaseFishingLine FishingLine => _fishingLine;
    public BaseFishingReel FishingReel =>  _fishingReel;
    public BasePole Pole => _pole;

    public void Initialize(BaseHook hook, BaseBobber bobber, BaseFishingLine fishingLine,
                                              BaseFishingReel fishingReel, BasePole pole,
                                              BaseFishingPoleController fishingPoleController)
    {
        _hook = hook;
        _bobber = bobber;
        _fishingLine = fishingLine;
        _fishingReel = fishingReel;
        _pole = pole;
        _fishingPoleController = fishingPoleController;

        isInitialized = true;
    }
}
