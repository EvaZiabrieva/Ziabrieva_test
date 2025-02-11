using UnityEngine;

public class FishFactory 
{
    public Fish CreateFish(FishData data, BaseFishView view)
    {
        return new Fish(data, view);
    }
}
