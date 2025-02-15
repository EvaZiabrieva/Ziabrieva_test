using System;

[Serializable]
public class FishingReelConfig : BaseConfig
{
    public float roundLength;
    public FishingReelConfig(string id, float roundLength) : base(id)
    {
        this.roundLength = roundLength;
    }
}
