using UnityEngine;

public abstract class BaseHookView
{
    protected HookVisualsContainer _hookVisualsContainer;
    protected GameObject _attachableVisuals;
    protected IHookAttachable _hookAttachable;

    public BaseHookView(HookVisualsContainer prefab)
    {
        _hookVisualsContainer = prefab;
    }

    public abstract void Initialize(Vector3 spawnPosition);

    public abstract void Attach(IHookAttachable attachable);

    public abstract void RemoveAttachment();
}
