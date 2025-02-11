using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class CollisionByLayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public event Action OnWaterDetected;
    public bool IsActive { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if(!IsActive)
        {
            return;
        }

        if (layerMask.Contains(other.gameObject.layer))
        {
            OnWaterDetected?.Invoke();
        }
    }
}
