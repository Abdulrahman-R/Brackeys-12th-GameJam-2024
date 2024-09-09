using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjSM : StormyMode
{

    // Start is called before the first frame update
    protected override void Start()
    {
        GameController.Instance.modeSwitcher.OnSwitchMode += OnStromyMode;
    }

}
