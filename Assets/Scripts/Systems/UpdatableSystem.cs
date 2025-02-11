using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatableSystem : MonoBehaviour, ISystem
{
    private List<IUpdatable> _updatables;
    public bool IsInitialized => _updatables != null;

    public void Initialize()
    {
        _updatables = new List<IUpdatable>();
    }

    public void Shutdown()
    {
        _updatables.Clear();
    }

    public void RegisterUpdatable(IUpdatable updatable)
    {
        if (_updatables.Contains(updatable))
        {
            Debug.LogWarning($"Updatable {updatable} is already registred");
            return;
        }

        _updatables.Add(updatable);
    }

    public void UnRegisterUpdatable(IUpdatable updatable)
    {
        if (!_updatables.Contains(updatable))
        {
            Debug.LogWarning($"Updatable {updatable} is not registred yet");
            return;
        }

        _updatables.Remove(updatable);
    }

    private void Update()
    {
        if (!IsInitialized)
            return;

        foreach (IUpdatable updatable in _updatables.ToArray())
        {
            updatable.Update();
        }
    }
}
