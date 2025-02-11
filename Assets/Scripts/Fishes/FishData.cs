using System;

[Serializable]
public enum Rarity
{
    Common, Rare,
}

[Serializable]
public class FishData
{ 
    public string Id { get; private set; }
    public float Weight {  get; private set; }
    public float Strength { get; private set; }
    public int Points { get; private set; }
    public int Level { get; private set; }
    public Rarity Rarity { get; private set; }

    public FishData(string id, float weight, float strength, 
                    int points, int level, Rarity rarity)
    {
        Id = id;
        Weight = weight;
        Strength = strength;
        Points = points;
        Level = level;
        Rarity = rarity;
    }
}
