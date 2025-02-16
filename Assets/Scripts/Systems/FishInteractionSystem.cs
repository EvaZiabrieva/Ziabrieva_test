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
        float baitAttractiveness = 0;
        // TODO: create configs system, choose random fish
        if (baits == null || baits.Count == 0)
        {
            return;
        }

        foreach (BaseBait bait in baits)
        {
            baitAttractiveness += bait.AttractivenessStrenght;
        }

        _currentFish = _spawnSystem.CreateFish(parent);
        float delay = Random.Range(_currentFish.Data.BehaviourData.BitDelayRange.min, _currentFish.Data.BehaviourData.BitDelayRange.max) / baitAttractiveness;

        StartCoroutine(WaitUntillFollowCo(delay));

        _baits.AddRange(baits);
    }

    private IEnumerator WaitUntillFollowCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        _currentFish.Controller.Initialize();
    }
    public void InvokeOnFishBiteTheBait(float strength)
    {
        OnFishBitTheBait?.Invoke(strength);
    }

    public void InvokeOnFishBit(Fish fish)
    {
        OnFishBit?.Invoke(fish);
    }
}
