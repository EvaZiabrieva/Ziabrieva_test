using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationStartUp : MonoBehaviour
{
    [SerializeField] private GameObject _systemsHolder;
    
    private void Awake()
    {
        SystemsContainer.Initialize(_systemsHolder.GetComponents<ISystem>());
    }
    private void Start()
    {
        SystemsContainer.GetSystem<SpawnSystem>().CreateFishingPole();
    }
    private void OnDestroy()
    {
        SystemsContainer.Shutdown();
    }
}
