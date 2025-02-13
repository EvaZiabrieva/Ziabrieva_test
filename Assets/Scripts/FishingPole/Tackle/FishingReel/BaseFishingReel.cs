using UnityEngine;

public abstract class BaseFishingReel
{
    protected BaseFishingReelView _view;
    protected float _roundLenght;
    public float RoundLenght => _roundLenght;
    public abstract void SetRange(float minAngle, float maxAngle);
    public abstract float GetLength();
    public abstract float SetAngle(float angle);
    public abstract void ApplyTension(float tension);
}
