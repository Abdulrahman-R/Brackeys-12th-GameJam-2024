using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltController : MonoBehaviour
{
    [SerializeField] private float _beltSpeed;
    [SerializeField] private float _maxBeltSpeed;
    [SerializeField] private float _currBeltSpeed;
    [SerializeField] private bool _reversed;

    [SerializeField] SurfaceEffector2D[] _effector2Ds;
    SurfaceEffector2D _currEffector;
    // Start is called before the first frame update
    void Start()
    {
        _currEffector = _effector2Ds[0];

        if (_reversed)
        {
            _currBeltSpeed = _beltSpeed * -1;
        }
        else
        {
            _currBeltSpeed = _beltSpeed;
        }

        if(_currEffector != null)
        {
            _currEffector.speed = _currBeltSpeed;
        }

        GameController.Instance.decisionTimeCounter.onTimeMultiplier += UpdateBeltSpeed;
        GameController.Instance.modeSwitcher.OnSwitchMode += ChangeBulit;
    }


    private void UpdateBeltSpeed()
    {
        
        _currBeltSpeed = _beltSpeed * GameController.Instance.decisionTimeCounter._timeMultiplier;

        _currBeltSpeed = Mathf.Clamp(_currBeltSpeed, _beltSpeed, _maxBeltSpeed);
    }
    
    private void ChangeBulit()
    {
        _currEffector = _effector2Ds[1];
        UpdateBeltSpeed();
    }
}
