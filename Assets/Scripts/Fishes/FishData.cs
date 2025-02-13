using System;
using UnityEngine;

[Serializable]
public enum Rarity
{
    Common, Rare,
}

[Serializable]
public class FishData
{ 
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public float Weight {  get; private set; }
    [field: SerializeField] public int Points { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public Rarity Rarity { get; private set; }
    [field: SerializeField] public FishBehaviourData BehaviourData { get; private set; }

    public FishData(string id, float weight, int points, int level, Rarity rarity)
    {
        Id = id;
        Weight = weight;
        Points = points;
        Level = level;
        Rarity = rarity;
    }
}

[Serializable]
public class FishBehaviourData
{
    [field: SerializeField] public float Strength { get; private set; }
    [field: SerializeField] public RangeFloat BitDelayRange { get; private set; }
    [field: SerializeField] public RangeInt BitesCountRange { get; private set; }
    [field: SerializeField] public RangeFloat BitesDelayRange { get; private set; }
    [field: SerializeField] public RangeFloat ChangeDirectionDelayRange { get; private set; }
    [field: SerializeField] public RangeFloat XDirectionRange { get; private set; }
    [field: SerializeField] public RangeFloat ZDirectionRange { get; private set; }
}
