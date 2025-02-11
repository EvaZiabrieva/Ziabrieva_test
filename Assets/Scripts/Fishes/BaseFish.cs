using System;
using UnityEngine;

/// <summary>
/// Possibility to extned fish with unique data or logic
/// </summary>
[Serializable]
public abstract class BaseFish
{
    [SerializeField] protected FishData _fishData;
    [SerializeField] protected BaseFishView _view;
    public FishData Data => _fishData;

    protected BaseFish(FishData data, BaseFishView view)
    {
        _fishData = data;
        _view = view;
    }
}
