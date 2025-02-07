using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour, ISystem
{
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private GameObject _pole;
    [SerializeField] private GameObject _fishingReel;
    [SerializeField] private GameObject _fishingLine;
    [SerializeField] private GameObject _hook;
    [SerializeField] private GameObject _bobber;

    private FishingPoleFactory _fishingPoleFactory;

    public bool IsInitialized => _fishingPoleFactory != null;

    public void Initialize()
    {
        _fishingPoleFactory = new FishingPoleFactory();
    }

    public void CreateFishingPole()
    {
        _fishingPoleFactory.CreateFishingPole(_pole, _fishingReel, _fishingLine, _hook, _bobber, _spawnPoint);
    }

    public void Shutdown()
    {
        _fishingPoleFactory.RemoveFishingPole();
    }
}
