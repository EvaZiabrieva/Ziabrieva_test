using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInteractionSystem : MonoBehaviour, ISystem
{
    [SerializeField] private List<Fish> _fishes;

    public bool IsInitialized => _fishes != null;

    public event System.Action<float> OnFishBitTheBait;
    public event System.Action<Fish> OnFishBit;

    private SpawnSystem _spawnSystem;
    private List<BaseBait> _baits = new List<BaseBait>();
    private Fish _currentFish;

    public void Initialize()
    {
        _fishes = new List<Fish>();
        _spawnSystem = SystemsContainer.GetSystem<SpawnSystem>();
    }

    public void Shutdown()
    {
        _fishes = null;
    }

    public void SetupFish(List<BaseBait> baits, Transform parent)
    {
        // TODO: create configs system, choose random fish
        if (baits == null || baits.Count == 0)
        {
            return;
        }

        _currentFish = _spawnSystem.CreateFish(parent);

        _baits.AddRange(baits);
    }
    public void StartBite()
    {
        FishBehaviourData data = _currentFish.Data.BehaviourData;

        float baitAttractiveness = 0;

        foreach (BaseBait bait in _baits)
        {
            baitAttractiveness += bait.AttractivenessStrenght;
        }

        float delay = Random.Range(data.BitDelayRange.min, data.BitDelayRange.max) / baitAttractiveness;
        int bitesCount = Random.Range(data.BitesCountRange.min, data.BitesCountRange.max + 1);

        StartCoroutine(DelayedBitingCo(_currentFish, delay, bitesCount, data.BitesDelayRange));
    }

    private IEnumerator DelayedBitingCo(Fish fish, float delay, int bitesCount, RangeFloat bitesDelayRange)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < bitesCount; i++)
        {
            OnFishBitTheBait?.Invoke(fish.Data.BehaviourData.Strength);
            float bitesDelay = Random.Range(bitesDelayRange.min, bitesDelayRange.max);
            yield return new WaitForSeconds(bitesDelay);
        }

        fish.OnBit();
        OnFishBit?.Invoke(fish);
    }
}
