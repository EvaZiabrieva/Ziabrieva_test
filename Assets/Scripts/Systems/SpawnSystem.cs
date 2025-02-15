using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour, ISystem
{
    [Header("Fishing Pole")]
    [SerializeField] private Transform _poleSpawnPoint;
    [SerializeField] private GameObject _pole;
    [SerializeField] private GameObject _fishingReel;
    [SerializeField] private GameObject _fishingLine;
    [SerializeField] private GameObject _hook;
    [SerializeField] private GameObject _bobber;

    [Header("Fish")]
    [SerializeField] private Fish _fishObject;

    private FishingPoleFactory _fishingPoleFactory;
    private FishFactory _fishFactory;

    public bool IsInitialized => _fishingPoleFactory != null;

    public void Initialize()
    {
        _fishingPoleFactory = new FishingPoleFactory();
        _fishFactory = new FishFactory();
    }

    public void CreateFishingPole()
    {
        _fishingPoleFactory.CreateFishingPole(_pole, _fishingReel, _fishingLine, _hook, _bobber, _poleSpawnPoint);
    }

    public Fish CreateFish(Vector3 spawnPosition)
    {
        // TODO: get fish data from configs
        return _fishFactory.CreateFish(_fishObject, spawnPosition);
    }

    public void Shutdown()
    {
        _fishingPoleFactory.RemoveFishingPole();
    }
}
