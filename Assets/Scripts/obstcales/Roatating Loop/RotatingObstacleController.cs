using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacleController : StormyMode
{
    [Header("Rotation Configs:")]
    [SerializeField]  private float _rotationSpeed = 100f;
    [SerializeField] private float _maxRotationSpeed;
    [SerializeField]private float _currRotationSpeed;
    [SerializeField] private bool _rotateClockwise = true; 
    bool _rotaionAllowed;

 
   protected override void Start()
    {
        _rotaionAllowed = true;
        _currRotationSpeed = _rotationSpeed;

        GameController.Instance.decisionTimeCounter.onTimeMultiplier += UpdateRotationSpeed;
        GameController.Instance.modeSwitcher.OnSwitchMode += OnStromyMode;
    }
    protected override void Update()
    {
        if (_rotaionAllowed) 
        {
            Roatate();
        } 
    }


    void Roatate()
    {
        float direction = _rotateClockwise ? 1f : -1f;
        transform.Rotate(Vector3.forward, direction * (_currRotationSpeed) * Time.deltaTime);
    }

    // Method to set rotation direction to clockwise
    public void SetClockwise(bool isClockwise)
    {
        _rotateClockwise = isClockwise;
    }

    private void UpdateRotationSpeed()
    {
        _currRotationSpeed = _rotationSpeed * GameController.Instance.decisionTimeCounter._timeMultiplier;

        _currRotationSpeed = Mathf.Clamp(_currRotationSpeed, _rotationSpeed, _maxRotationSpeed);
    }
}
