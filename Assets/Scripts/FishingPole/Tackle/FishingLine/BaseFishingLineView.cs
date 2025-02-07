using UnityEngine;

public abstract class BaseFishingLineView 
{
    protected float _currentLength;

    public abstract void Reel(float speed);

    public abstract void UnReel(float speed);

    public void SetLenght(float lenght) => _currentLength = lenght;
}
