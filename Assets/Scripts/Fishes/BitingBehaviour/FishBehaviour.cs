using UnityEngine;

public class FishBehaviour : BaseFishBehaviour
{
    private FishPullingData _pullingData;

    public override void Bite()
    {
        
    }

    public override void Pull(Vector3 direction, float strength)
    {
        _pullingData = new FishPullingData(direction, strength);
    }

    public override void Release()
    {
        
    }

    public override FishPullingData GetPullingData() => _pullingData;
}
