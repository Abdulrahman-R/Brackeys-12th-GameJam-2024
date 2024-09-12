using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjMovementSate
{
    Moving,
    Waiting,
    Stopped
}
public enum MovementMode
{
    Continuous,
    Intermittent
}
public class ObjMovementFSM : MonoBehaviour
{
    private ObjMovementSate _movementCurrState;

    #region Movemonet Configs
    [Header("Movemonet Configs:")]
    [SerializeField] private MovementMode _movementMode;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _currSpeed;
    [SerializeField] private float _waitTime = 1f;
    [SerializeField] private float _minWaitTime;
    [SerializeField] private float _currWaitTime;
    [SerializeField] private Transform[] _points;
    [SerializeField] Transform _pointsHolder;
    private int _targetIndex = 0;
    private bool _isReversing = false;
    private bool _movementAllowed;
    private bool _timerStarted;
    #endregion



    

    void Start()
    {
        _currSpeed = _speed;
        _movementAllowed = true;

        _pointsHolder.parent = null;
        ChangeState(ObjMovementSate.Moving);
        
       

        // Start at the first point
        // transform.position = _points[0].position;

        GameController.Instance.decisionTimeCounter.onTimeMultiplier += UpdateSpeed;
        GameController.Instance.decisionTimeCounter.onTimeMultiplier += UpdateWaitTime;
    }

    void Update()
    {
        if (!_movementAllowed) { return; }
        if (_points.Length == 0)
        {
            return;
        }
        TransitionState();
        WaitTimer();
    }

    public void ChangeState(ObjMovementSate state)
    {
        _movementCurrState = state;
    }

    public void TransitionState()
    {
        switch (_movementCurrState)
        {
            case ObjMovementSate.Moving: OnMoving(); break;
            case ObjMovementSate.Waiting: OnWaiting(); break;
        }
    }

    private void OnMoving()
    {
        if (_points.Length == 0)
            return;
        MoveTowardsTarget();
    }

    public void OnWaiting()
    {
        if (!_timerStarted)
        {
            StartWaitTimer();
        }
        else if(_currWaitTime <= 0)
        {
            _timerStarted = false;
            UpdateTargetIndexIntermittent();
            ChangeState(ObjMovementSate.Moving);
        }
    }

   


    private void MoveTowardsTarget()
    {
        // Move towards the target point
        transform.position = Vector3.MoveTowards(transform.position, _points[_targetIndex].position, _currSpeed * Time.deltaTime);

        // Check if the platform has reached the target point
        if (Vector3.Distance(transform.position, _points[_targetIndex].position) < 0.01f)
        {
            if (_movementMode == MovementMode.Continuous)
            {
                UpdateTargetIndexContinuous();
            }
            else if (_movementMode == MovementMode.Intermittent)
            {
                ChangeState(ObjMovementSate.Waiting);
            }
        }
    }

    private void UpdateTargetIndexContinuous()
    {
        _targetIndex++;
        if (_targetIndex >= _points.Length)
        {
            _targetIndex = 0; // Loop back to the first point
        }
    }

    private void UpdateTargetIndexIntermittent()
    {
        if (_isReversing)
        {
            _targetIndex--;
            if (_targetIndex < 0)
            {
                _targetIndex = 1; // Ensure we move to the second point after reversing
                _isReversing = false;
            }
        }
        else
        {
            _targetIndex++;
            if (_targetIndex >= _points.Length)
            {
                _targetIndex = _points.Length - 2; // Ensure we move to the second-to-last point after reversing
                _isReversing = true;
            }
        }
    }

 

    private void StartWaitTimer()
    {
        _timerStarted = true;
        _currWaitTime = _waitTime;
    }
    private void WaitTimer()
    {
        if(_currWaitTime <= 0) { return; }

        _currWaitTime -= Time.deltaTime;

    }


    private void OnDrawGizmos()
    {
        if (_points.Length > 1)
        {
            for (int i = 0; i < _points.Length - 1; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(_points[i].position, _points[i + 1].position);
            }
        }
    }

    private void UpdateSpeed()
    {
        _speed *= GameController.Instance.decisionTimeCounter._timeMultiplier;
        _speed = Mathf.Clamp(_speed, _speed, _maxSpeed);
        _currSpeed = _speed;
    }

    private void UpdateWaitTime()
    {
        _waitTime /= GameController.Instance.decisionTimeCounter._timeMultiplier;
        _waitTime = Mathf.Clamp(_waitTime, _minWaitTime, _waitTime);
    }
}

