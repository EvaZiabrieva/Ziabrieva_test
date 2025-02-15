using System;

[Serializable]
public class FishingLineConfig : BaseConfig
{
    public float maxLength;
    public float strength;

    public FishingLineConfig(string id, float maxLength, float strength) : base(id)
    {
        this.maxLength = maxLength;
        this.strength = strength;
    }
}
