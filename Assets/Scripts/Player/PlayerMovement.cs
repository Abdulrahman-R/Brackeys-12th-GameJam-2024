using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float effectorSpeed { get; set; }
    public Rigidbody2D _rb { get; private set; }

    #region movment configs
    [Header("Movment Configs")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _currSpeed;
    [SerializeField] private float _smoothTime;
    private Vector2 currentInput;
    private Vector2 smoothInputVelocity;
    private Vector2 moveInput;
    #endregion

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currSpeed = 0;
        GameController.Instance.decisionTimeCounter.onTimeMultiplier += UpdateMoveSpeed;
    }

    void Update()
    {
        moveInput = ListenForMovementInputs();
    }

    private void FixedUpdate()
    {
        Move(moveInput);
    }

    private Vector2 ListenForMovementInputs()
    {
        if (!PlayerController.Instance._canAct)
        {
            return Vector2.zero;
        }
        else
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), 0f);
            currentInput = Vector2.SmoothDamp(currentInput, input, ref smoothInputVelocity, _smoothTime);
            // Only calculate the horizontal movement, leave vertical movement alone
            Vector2 moveInput = new Vector2(currentInput.x * _currSpeed, _rb.velocity.y);
            return moveInput;
        }
    }

    private void Move(Vector2 moveInput)
    {
        // Apply movement only to the X-axis
        _rb.velocity = new Vector2(moveInput.x + effectorSpeed, _rb.velocity.y);
    }

    private void UpdateMoveSpeed()
    {
        _currSpeed = _moveSpeed * GameController.Instance.decisionTimeCounter._timeMultiplier;
        _currSpeed = Mathf.Clamp(_currSpeed, _moveSpeed, _maxSpeed);
    }
}
