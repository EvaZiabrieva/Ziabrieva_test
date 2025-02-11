using System;

[Serializable]
public class Fish : BaseFish
{
    public Fish(FishData data, BaseFishView view) : base(data, view) {}
}
