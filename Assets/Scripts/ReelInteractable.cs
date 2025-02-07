using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ReelInteractable : XRBaseInteractable
{
    [SerializeField] private Transform reelTransform;

    public UnityEvent<float> onReelRotated;
    private float currentAngle = 0.0f;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        currentAngle = FindReelAngle();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        currentAngle = FindReelAngle();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
                RrotateReel();
        }
    }
    
    private void RrotateReel()
    {
        float totalAngle = FindReelAngle();
        float angleDifference = currentAngle - totalAngle;
        Vector3 rotation = new Vector3(-angleDifference, 0, 0);
        reelTransform.Rotate(rotation);

        currentAngle = totalAngle;
        onReelRotated?.Invoke(angleDifference);
    }

    private float FindReelAngle()
    {
        float totalAngle = 0;

        foreach(IXRActivateInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindLocalPoint(interactor.transform.rotation.eulerAngles);
            totalAngle += ConvertToAngle(direction) * FindRotationSensativity();
        }

        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector2 position) =>
        transform.InverseTransformPoint(position);

    private float ConvertToAngle(Vector2 direction) =>
        Vector2.SignedAngle(transform.up, direction);

    private float FindRotationSensativity() =>
        1f / interactorsSelecting.Count;
}
