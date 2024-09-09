using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPlatformBrain : StormyMode
{
    [Header("PopUp Platform Brain Configs:")]
    [SerializeField] private float _popingDlayTime;
    public float GetpopingDlayTime() { return _popingDlayTime; }
    [SerializeField] float _minDelayTime;
    // Start is called before the first frame update
    protected override void Start()
    {
        GameController.Instance.decisionTimeCounter.onTimeMultiplier += UpdatePopingDlayTime;
        GameController.Instance.modeSwitcher.OnSwitchMode += OnStromyMode;
    }

 

    private void UpdatePopingDlayTime()
    {
        _popingDlayTime /= GameController.Instance.decisionTimeCounter._timeMultiplier;
        _popingDlayTime = Mathf.Clamp(_popingDlayTime, _minDelayTime, _popingDlayTime);
    }
}
