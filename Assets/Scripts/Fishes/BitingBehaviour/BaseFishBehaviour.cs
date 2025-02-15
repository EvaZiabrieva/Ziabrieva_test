using UnityEngine;

public abstract class BaseFishBehaviour 
{
    protected FishBehaviourData _behaviourData;

    protected BaseFishBehaviour(FishBehaviourData data)
    {
        _behaviourData = data;
    }

    public abstract void Bite();
    public abstract void Pull(Vector3 direction);
    public abstract void Release();
}
