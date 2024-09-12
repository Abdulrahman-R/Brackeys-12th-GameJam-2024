using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerJump : MonoBehaviour
{

    #region jump configs
    [Header("Jump Configs")]
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _maxJumpForce;
    private float _currJumpForce;
    public float GetMaxJumpPower() { return _jumpForce; }
    [SerializeField] Transform _groundCheck;
    [SerializeField] Vector2 _groundCheckSize = new Vector2(0.5f, 0.1f);
    [SerializeField] private float _extraGravity = 700f;
    [SerializeField] private float _gravityDelay = 0.2f;
    [SerializeField] LayerMask _groundLayer;
    private float _timeInAir;
    private bool _jumpRequest;
    [SerializeField] private float _coyoteTime;
    private float _coyoteTimer;
    private Rigidbody2D _rb;
    #endregion
    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currJumpForce = 0;

        GameController.Instance.decisionTimeCounter.onTimeMultiplier += UpdateJumpForce;

    }

    // Update is called once per frame
    void Update()
    {
        ListenForJumpRequests();
        CoyoteTimer();
        GravityDelay();
    }

    private void FixedUpdate()
    {
        if (_jumpRequest)
        {
            Jump();
            ResetJumpConfigs();
        }

        ExtraGravity();
    }

    private void ListenForJumpRequests()
    {
        if (!PlayerController.Instance._canAct) { return; }
        if (Input.GetButtonDown("Jump") && (_coyoteTimer > 0))
        {
            
            _jumpRequest = true;
        }
    }

    private void CoyoteTimer()
    {
        if (IsGrounded())
        {
            _coyoteTimer = _coyoteTime;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }
    }

    private void Jump()
    {
        _rb.AddForce(new Vector2(0, _currJumpForce), ForceMode2D.Impulse);
    }

    private void GravityDelay()
    {
        if (!IsGrounded())
        {
            _timeInAir += Time.deltaTime;
        }
        else
        {
            _timeInAir = 0f;
        }
    }

    private void ExtraGravity()
    {
        if (_timeInAir > _gravityDelay)
        {
            _rb.AddForce(new Vector2(0f, -_extraGravity * Time.deltaTime));
        }
    }

    private void ResetJumpConfigs()
    {
        _jumpRequest = false;
        _coyoteTimer = 0;
    }

    public bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_groundCheck.position, _groundCheckSize, 0, _groundLayer);
        return colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundCheck.position, _groundCheckSize);
    }

    private void UpdateJumpForce()
    {
        _currJumpForce = _jumpForce * GameController.Instance.decisionTimeCounter._timeMultiplier;

        _currJumpForce = Mathf.Clamp(_currJumpForce, _jumpForce, _maxJumpForce);
    }
}
