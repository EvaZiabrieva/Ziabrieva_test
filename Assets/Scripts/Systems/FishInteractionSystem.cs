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
        _currentFish.Controller.Initialize();

        _baits.AddRange(baits);
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
