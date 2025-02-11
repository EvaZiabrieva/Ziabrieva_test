using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachDetector : MonoBehaviour
{
    public event Action<IHookAttachable> OnAttach;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IHookAttachable attachable))
        {
            if (attachable.ReadyToAttach)
            {
                OnAttach?.Invoke(attachable);
            }
        }
    }
}
