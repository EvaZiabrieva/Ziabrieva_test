using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PoleInteactor : XRGrabInteractable
{
    public event Action onGrab;
    protected override void Grab()
    {
        base.Grab();
        onGrab?.Invoke();
    }
}
