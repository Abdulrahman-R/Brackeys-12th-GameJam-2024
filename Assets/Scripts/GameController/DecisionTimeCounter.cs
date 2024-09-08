using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DecisionTimeCounter : MonoBehaviour
{
    public float _timeMultiplier { get; private set; }

    public float _currentTime { get; private set; }
    bool _playerMoved;
    bool _hasTimerStarted;

    public Action onTimeMultiplier;
    public Action onTimeChange;
    // Start is called before the first frame update
    void Start()
    {
        _timeMultiplier = 0;
        _playerMoved = false;
        _hasTimerStarted = false;

        GameController.Instance.modeSwitcher.OnSwitchMode += StopTimer;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerStat();
        UpdateTimer();
    }

    private void CheckPlayerStat() 
    {
        if (_playerMoved) { return; }

        if(Input.GetAxis("Horizontal") != 0)
        {
            _playerMoved = true;
            StartTimer();
        }
    
    }

    private void StartTimer()
    {
        _timeMultiplier = 1;
        _hasTimerStarted = true;
        onTimeMultiplier?.Invoke();
    }

    private void UpdateTimer()
    {
        if (!_hasTimerStarted) { return; }
        _currentTime += Time.deltaTime;
        onTimeChange?.Invoke();
    }

    private void StopTimer()
    {
        _hasTimerStarted = false;
        CalculateTimeMultiplier();
    }
    private void CalculateTimeMultiplier() 
    {
        _timeMultiplier = 1 + (_currentTime / 10f);
        onTimeMultiplier?.Invoke();
    }

}
