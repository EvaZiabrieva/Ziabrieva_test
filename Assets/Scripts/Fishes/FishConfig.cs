using System;

[Serializable]
public class FishConfig
{
    public string Id { get; private set; }
    public RangeFloat WeightRange { get; private set; }
    public int Level { get; private set; }
    public float BaseStrength { get; private set; }
    public int BasePointsValue { get; private set; }
}

//TODO: move to separate file
[Serializable]
public struct RangeInt
{
    public int min;
    public int max;
}

//TODO: move to separate file
[Serializable]
public struct RangeFloat
{
    public float min;
    public float max;
}
