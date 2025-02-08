using UnityEngine;

public abstract class BaseFishingReel
{
    protected BaseFishingReelView _view;
    protected float _roundLenght;
    public abstract float GetAngle();
    public abstract void ReelOnCast(Vector3 direction, float force);
}
