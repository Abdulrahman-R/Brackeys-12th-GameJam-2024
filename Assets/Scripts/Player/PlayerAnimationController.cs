using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    Idle,
    Moving,
    Stopping,
    Jumping
}


public class PlayerAnimationController : MonoBehaviour
{
    private PlayerState _currentState;

    [SerializeField] private Animator[] _animators;
    [SerializeField] private SpriteRenderer[] _renderers;
    private Animator _currAnimator;
    private SpriteRenderer _currRenderer;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currAnimator = _animators[0];
        _currRenderer = _renderers[0];
        _currentState = PlayerState.Idle;

        GameController.Instance.modeSwitcher.OnSwitchMode += OnStormyMode;
    }

    void Update()
    {
        TransitionState();
        HandleSpriteFlip();
    }

    private void HandleSpriteFlip()
    {
        if (_currRenderer != null)
        {
            if (_rb.velocity.x > 0)
            {
                _currRenderer.flipX = false;
            }else if(_rb.velocity.x < 0)
            {
                _currRenderer.flipX = true;
            }
                
        }
    }

    public void ChangeState(PlayerState newState)
    {
        if (_currentState != newState)
        {
            _currentState = newState;
        }
    }

    public void TransitionState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle: OnIdle(); break;
            case PlayerState.Moving: OnMoving(); break;
            case PlayerState.Stopping: OnStopping(); break;
            case PlayerState.Jumping: OnJumping(); break;
        }
    }

    private void OnIdle()
    {
        if (!PlayerController.Instance.playerJump.IsGrounded())
        {
            _currAnimator.SetBool("isJumping", true);  // Enable jumping
            _currAnimator.SetBool("isIdle", false);
            ChangeState(PlayerState.Jumping);
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            _currAnimator.SetBool("isMoving", true);  // Enable moving
            _currAnimator.SetBool("isIdle", false);   // Disable idle
            ChangeState(PlayerState.Moving);
        }
        else
        {
            _currAnimator.SetBool("isIdle", true);  // Enable idle
            _currAnimator.SetBool("isMoving", false);  // Disable moving
        }
    }

    private void OnMoving()
    {
        if (!PlayerController.Instance.playerJump.IsGrounded())
        {
            _currAnimator.SetBool("isJumping", true);  // Enable jumping
            ChangeState(PlayerState.Jumping);
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            _currAnimator.SetBool("isMoving", false);   // Disable moving
            _currAnimator.SetBool("isStopping", true);  // Enable stopping
            ChangeState(PlayerState.Stopping);
        }
    }

    private void OnStopping()
    {
        if (!PlayerController.Instance.playerJump.IsGrounded())
        {
            _currAnimator.SetBool("isJumping", true);  // Enable jumping
            ChangeState(PlayerState.Jumping);
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            _currAnimator.SetBool("isMoving", true);   // Enable moving
            _currAnimator.SetBool("isStopping", false);  // Disable stopping
            ChangeState(PlayerState.Moving);
        }
        else if (Mathf.Abs(_rb.velocity.magnitude - Mathf.Abs(PlayerController.Instance.playerMovement.effectorSpeed)) <= 0.1f)
        {
            _currAnimator.SetBool("isIdle", true);   // Enable idle
            _currAnimator.SetBool("isStopping", false);  // Disable stopping
            ChangeState(PlayerState.Idle);
        }
    }

    private void OnJumping()
    {
        if (PlayerController.Instance.playerJump.IsGrounded())
        {
         
            if (Input.GetAxis("Horizontal") != 0)
            {
                _currAnimator.SetBool("isJumping", false);
                _currAnimator.SetBool("isMoving", true);
                ChangeState(PlayerState.Moving);
            }
            else
            {
                _currAnimator.SetBool("isJumping", false);  // Disable jumping
                _currAnimator.SetBool("isIdle", true);   // Return to idle after landing
                ChangeState(PlayerState.Idle);
            }
        }
    }

    private void OnStormyMode()
    {
        _currAnimator.gameObject.SetActive(false);
        _animators[1].gameObject.SetActive(true);
        _currAnimator = _animators[1];
        _currRenderer = _renderers[1];
    }
}
