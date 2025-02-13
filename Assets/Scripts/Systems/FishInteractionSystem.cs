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

    public void Initialize()
    {
        _fishes = new List<Fish>();
        _spawnSystem = SystemsContainer.GetSystem<SpawnSystem>();
    }

    public void Shutdown()
    {
        _fishes = null;
    }

    public void SetupFish(List<BaseBait> baits, Vector3 position)
    {
        // TODO: create configs system, choose random fish
        Fish fish = _spawnSystem.CreateFish(position);
        FishBehaviourData data = fish.Data.BehaviourData;

        if (baits == null || baits.Count == 0)
        {
            OnFishBit?.Invoke(fish);
            return;
        }

        float baitAttractiveness = 0;

        foreach (BaseBait bait in baits)
        {
            baitAttractiveness += bait.AttractivenessStrenght;
        }

        float delay = Random.Range(data.BitDelayRange.min, data.BitDelayRange.max) / baitAttractiveness;
        int bitesCount = Random.Range(data.BitesCountRange.min, data.BitesCountRange.max + 1);

        StartCoroutine(DelayedBitingCo(fish, delay, bitesCount, data.BitesDelayRange));
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
