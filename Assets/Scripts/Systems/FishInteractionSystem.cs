using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInteractionSystem : MonoBehaviour, ISystem
{
    public bool IsInitialized => _spawnSystem != null;

    public event System.Action<float> OnFishBitTheBait;
    public event System.Action<Fish> OnFishBit;

    private SpawnSystem _spawnSystem;
    private FishingProgressSystem _progressSystem;
    private List<BaseBait> _baits = new List<BaseBait>();
    private Fish _currentFish;

    public void Initialize()
    {
        _spawnSystem = SystemsContainer.GetSystem<SpawnSystem>();
    }

    public void Shutdown() 
    {
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
    private void StopFishing(bool isSuccessful)
    {
        _currentFish.Controller.Shutdown();
        _progressSystem.OnFishingFinished -= StopFishing;
    }
    public void InvokeOnFishBiteTheBait(float strength)
    {
        OnFishBitTheBait?.Invoke(strength);
    }

    public void InvokeOnFishBit(Fish fish)
    {
        OnFishBit?.Invoke(fish);
        //TODO: resolve conflict with FishingProgressSystem Init
        _progressSystem = SystemsContainer.GetSystem<FishingProgressSystem>();
        _progressSystem.OnFishingFinished += StopFishing;
    }
}
