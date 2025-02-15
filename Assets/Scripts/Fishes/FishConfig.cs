using System;
using UnityEngine;

[Serializable]
public class FishConfig : BaseConfig
{
    public float weight;
    public int points;
    public int level;
    public Rarity rarity;
    public FishBehaviourData behaviourData;

    public FishConfig(string id, float weight, int points, int level, 
                      Rarity rarity, FishBehaviourData behaviourData) : base (id)
    {
        this.weight = weight;
        this.points = points;
        this.level = level;
        this.rarity = rarity;
        this.behaviourData = behaviourData;
    }
}
