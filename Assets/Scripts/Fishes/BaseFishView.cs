using System;
using UnityEngine;


/// <summary>
/// Can be extended with additional view logic for other fish visual states
/// </summary>
[Serializable]
public abstract class BaseFishView 
{
    [SerializeField] protected GameObject _prefab;
    public GameObject Prefab => _prefab;
}
