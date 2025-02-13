using UnityEngine;

public abstract class BaseFishBehaviour 
{
    public abstract void Bite();
    public abstract void Pull(Vector3 direction, float strength);
    public abstract void Release();
    public abstract FishPullingData GetPullingData();
}

public struct FishPullingData
{
    public Vector3 direction;
    public float strength;

    public FishPullingData(Vector3 direction, float strength)
    {
        this.direction = direction;
        this.strength = strength;
    }
}
