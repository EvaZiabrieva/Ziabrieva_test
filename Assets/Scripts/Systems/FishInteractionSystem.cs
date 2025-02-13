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
    [SerializeField] private RangeFloat _bitesDelay;
    #endregion

    private FishFactory _factory;
    public bool IsInitialized => _fishes != null;

    public event System.Action OnFishBitTheBait;
    public event System.Action<BaseFish> OnFishBit;

    public void Initialize()
    {
        _fishes = new List<BaseFish>();
        _factory = new FishFactory();
    }

    public void Shutdown()
    {
        _fishes = null;
    }

    public void SetupFish(List<BaseBait> baits)
    {
        // TODO: create configs system, choose random fish
        Fish fish = _factory.CreateFish(_fishData, _fishView);
        float baitAttractiveness = 0;

        foreach (BaseBait bait in baits)
        {
            baitAttractiveness += bait.AttractivenessStrenght;
        }

        float delay = Random.Range(_bitDelayRange.min, _bitDelayRange.max) / baitAttractiveness;

        int bitesCount = Random.Range(_bitesCount.min, _bitesCount.max + 1);
    }

    private IEnumerator DelayedBitingCo(BaseFish fish, float delay, int bitesCount)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < bitesCount; i++)
        {
            OnFishBitTheBait?.Invoke();
            float bitesDelay = Random.Range(_bitesDelay.min, _bitesDelay.max);
            yield return new WaitForSeconds(bitesDelay);
        }

        fish.OnBit();
        OnFishBit?.Invoke(fish);
    }
}
