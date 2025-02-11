using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInteractionSystem : MonoBehaviour, ISystem
{
    [SerializeField] private List<BaseFish> _fishes;
    #region CONFIGS
    [SerializeField] private FishData _fishData;
    [SerializeField] private FishView _fishView;
    [SerializeField] private RangeFloat _bitDelayRange;
    [SerializeField] private RangeInt _bitesCount;
    #endregion

    private FishFactory _factory;
    public bool IsInitialized => _fishes != null;

    public void Initialize()
    {
        _fishes = new List<BaseFish>();
        _factory = new FishFactory();
    }

    public void Shutdown()
    {
        _fishes = null;
    }

    public void SetupFishBehaviour(BaseBait bait)
    {
        // TODO: create configs system, choose random fish
        Fish fish = _factory.CreateFish(_fishData, _fishView);
        float delay = Random.Range(_bitDelayRange.min, _bitDelayRange.max) / bait.AttractivenessStrenght;

        int bitesCount = Random.Range(_bitesCount.min, _bitesCount.max + 1);
    }

    
}
