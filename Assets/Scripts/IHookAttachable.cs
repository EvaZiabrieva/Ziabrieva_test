using UnityEngine;

public interface IHookAttachable 
{
    GameObject Visuals { get; }
    public bool ReadyToAttach { get; }
    public void OnAttach();
}
