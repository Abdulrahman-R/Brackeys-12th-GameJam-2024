using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementTracker : MonoBehaviour
{
    private Vector3 previousPosition;
    public Vector3 platformVelocity { get; private set; }   // The velocity vector
    public float platformSpeed { get; private set; }        // The speed (can be positive or negative)
    public Vector3 platformDirection { get; private set; }  // The normalized direction of movement

    [SerializeField] private Vector3 referenceDirection = Vector3.right;  // Reference direction (e.g., X-axis)

    public bool isMovingAlongX { get; private set; }        // Bool to check if the platform is moving along the X-axis
    public bool isMovingAlongY { get; private set; }        // Bool to check if the platform is moving along the Y-axis

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        Debug.Log(isMovingAlongX);
        // Calculate platform velocity based on position change
        platformVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;

        // Calculate direction as the normalized velocity vector
        if (platformVelocity != Vector3.zero)
        {
            platformDirection = platformVelocity.normalized;
        }

        // Use the dot product to determine the sign of the speed based on the reference direction
        platformSpeed = platformVelocity.magnitude * Mathf.Sign(Vector3.Dot(platformDirection, referenceDirection));

        // Determine whether the platform is moving more along the X-axis or Y-axis
        isMovingAlongX = Mathf.Abs(platformVelocity.x) > Mathf.Abs(platformVelocity.y);
        isMovingAlongY = Mathf.Abs(platformVelocity.y) > Mathf.Abs(platformVelocity.x);
    }
}
