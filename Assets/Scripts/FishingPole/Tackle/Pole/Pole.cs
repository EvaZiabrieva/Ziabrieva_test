using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : BasePole
{
    public Pole(float stregth, BasePoleView poleView)
    {
        _stregth = stregth;
        _view = poleView;
    }
}
