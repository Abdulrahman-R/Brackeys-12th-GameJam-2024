using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacleController : MonoBehaviour
{
    [SerializeField]  private float _rotationSpeed = 100f; // Speed of rotation
    [SerializeField] private bool _rotateClockwise = true; // Direction of rotation
    bool _rotaionAllowed;

 
    private void Start()
    {
        _rotaionAllowed = true;
    }
    void Update()
    {
        if (_rotaionAllowed) 
        {
            Roatate();
        } 
    }


    void Roatate()
    {
        float direction = _rotateClockwise ? 1f : -1f;
        transform.Rotate(Vector3.forward, direction * (_rotationSpeed) * Time.deltaTime);
    }

    // Method to set rotation direction to clockwise
    public void SetClockwise(bool isClockwise)
    {
        _rotateClockwise = isClockwise;
    }

    
}
