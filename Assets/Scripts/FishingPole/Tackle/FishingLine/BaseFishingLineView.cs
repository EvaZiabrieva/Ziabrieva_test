using UnityEngine;

public abstract class BaseFishingLineView 
{
    protected float _currentLength;
    protected float _maxLenght;
    public float MaxLength => _maxLenght;

    protected BaseFishingLineView(float maxLenght)
    {
        _maxLenght = maxLenght;
    }
    public abstract void Reel(float speed);

    public abstract void UnReel(float speed);

    public void SetLenght(float lenght) => _currentLength = lenght;
}
