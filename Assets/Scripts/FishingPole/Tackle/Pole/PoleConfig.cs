using System;

[Serializable]
public class PoleConfig : BaseConfig
{
    public float strength;
    public PoleConfig(string id, float strength) : base(id)
    {
        this.strength = strength;
    }
}
