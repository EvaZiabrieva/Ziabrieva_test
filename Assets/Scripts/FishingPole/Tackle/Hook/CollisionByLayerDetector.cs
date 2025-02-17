using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CollisionByLayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public event Action OnEnterDetected;
    public event Action OnExitDetected;
    public bool IsActive { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsActive)
        {
            return;
        }

        if (layerMask.Contains(other.gameObject.layer))
        {
            OnEnterDetected?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (layerMask.Contains(other.gameObject.layer))
        {
            OnEnterDetected?.Invoke();
        }
    }
}
