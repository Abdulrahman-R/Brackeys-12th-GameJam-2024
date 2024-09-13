using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    private CinemachineImpulseSource _impulseSource;
    // Start is called before the first frame update
    void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        GameController.Instance.modeSwitcher.OnSwitchMode += ShakeIt;
        PlayerController.Instance.playerDeath.onPlayerDeath += ShakeIt;
    }

    public void ShakeIt()
    {
        if(_impulseSource == null) { return; }
        _impulseSource.GenerateImpulse();
    }
}
