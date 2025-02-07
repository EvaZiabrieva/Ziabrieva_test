using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISystem
{
    void Initialize();
    void Shutdown();
    bool IsInitialized { get; }
}
