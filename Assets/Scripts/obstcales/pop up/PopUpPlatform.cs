using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PopUpPlatformState 
{
    PopedIn,
    Waiting,
    PopedOut
}

public class PopUpPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject _objToPop;
    [SerializeField] float _offsetTime;
    private float _currOffsetTime;
    [SerializeField] float _popingDlayTime;
    [SerializeField] float _minDelayTime;
    [SerializeField] private float _currPopingDlayTime;

   

    private PopUpPlatformState _CurrState;

    void Start()
    {
        GameController.Instance.decisionTimeCounter.onTimeMultiplier += UpdatePopingDlayTime;

        _currOffsetTime = _offsetTime;

        if (!_objToPop.activeInHierarchy)
        {
            ChangeState(PopUpPlatformState.PopedIn);
        }
        else
        {
            ChangeState(PopUpPlatformState.PopedOut);
        }
    }


    private void Update()
    {
        OffsetTimer();

        if (_currOffsetTime > 0) { return; }

        TransitionState();
        WaitTimer();
    }

    public void ChangeState(PopUpPlatformState  state)
    {
        _CurrState = state;
    }

    public void TransitionState()
    {
        switch (_CurrState)
        {
            case PopUpPlatformState.PopedIn: OnPopIn(); break;
            case PopUpPlatformState.Waiting: OnWaiting(); break;
            case PopUpPlatformState .PopedOut: OnPopOut(); break;
        }
    }

    private void OnPopIn()
    {
        _objToPop.SetActive(true);
        StartWaitTimer();
        ChangeState(PopUpPlatformState.Waiting);
       
    }

    private void OnPopOut()
    {
        _objToPop.SetActive(false);
        StartWaitTimer();
        ChangeState(PopUpPlatformState.Waiting);
    }

    private void OnWaiting()
    {
        if(_currPopingDlayTime <= 0)
        {
            if (!_objToPop.activeInHierarchy)
            {
                ChangeState(PopUpPlatformState.PopedIn);
            }
            else
            {
                ChangeState(PopUpPlatformState.PopedOut);
            }
        }
    }
  

    private void OffsetTimer()
    {
        if(_currOffsetTime <= 0) { return; }

        _currOffsetTime -= Time.deltaTime;
    }


    private void StartWaitTimer()
    {
        _currPopingDlayTime = _popingDlayTime;
    }
    private void WaitTimer()
    {
        if(_currPopingDlayTime <= 0) { return; }

        _currPopingDlayTime -= Time.deltaTime;
    }

    private void UpdatePopingDlayTime()
    {
        if(_CurrState == PopUpPlatformState.Waiting)
        {
            _popingDlayTime /= GameController.Instance.decisionTimeCounter._timeMultiplier;
            _popingDlayTime = Mathf.Clamp(_popingDlayTime, _minDelayTime, _popingDlayTime);
           // _currPopingDlayTime /= GameController.Instance.decisionTimeCounter._timeMultiplier;
         //   _currPopingDlayTime = Mathf.Clamp(_currPopingDlayTime, _minDelayTime, _currPopingDlayTime);
        }
        else
        {
            _popingDlayTime /= GameController.Instance.decisionTimeCounter._timeMultiplier;
            _popingDlayTime = Mathf.Clamp(_popingDlayTime, _minDelayTime, _popingDlayTime);
        }
    }

}
