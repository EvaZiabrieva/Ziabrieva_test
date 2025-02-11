using UnityEngine;

public abstract class BaseFishBehaviour 
{
    public abstract void Bite();
    public abstract void Pull(Vector3 direction, float strength);
    public abstract void Release();
}
