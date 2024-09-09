using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPattern : MonoBehaviour
{
    private PopUpPlatformBrain _popUpPlatformBrain;
    [SerializeField] GameObject[] _objsToPop;
    [SerializeField] float _offsetTime;
    private float _currOffsetTime;

    [SerializeField] private float _currPopingDlayTime;
    private int _popCounter;
    [SerializeField] private bool _popingUp;
    [SerializeField] private float _popPerTime;

    // Start is called before the first frame update
     void Start()
    {
        _popUpPlatformBrain = transform.parent.GetComponent<PopUpPlatformBrain>();
        _popCounter =0;
        _currOffsetTime = _offsetTime;
    }

    // Update is called once per frame
    void Update()
    {
        OffsetTimer();
        if (_currOffsetTime > 0) { return; }

        Poping();
        WaitTimer();
    }

    private void PopUp()
    {
        for( int i = 0; i < _popPerTime; i++)
        {
            _objsToPop[_popCounter].SetActive(true);
            _popCounter++;
        }
     
        if (_popCounter >= _objsToPop.Length)
        {
            _popCounter = 0;
            _popingUp = !(_popingUp);
        }

    }

    private void PopOut()
    {

        for (int i = 0; i < _popPerTime; i++)
        {
            _objsToPop[_popCounter].SetActive(false);
            _popCounter++;
        }
  
        if(_popCounter >= _objsToPop.Length)
        {
            _popCounter = 0;
            _popingUp = !(_popingUp);
        }
    }

    private void Poping()
    {
        if (_popingUp)
        {
            if (_currPopingDlayTime <= 0)
            {
                PopUp();
                StartWaitTimer();
            }
        }
        else
        {
            if (_currPopingDlayTime <= 0)
            {
                PopOut();
                StartWaitTimer();
            }
        }
       
    }

    private void StartWaitTimer()
    {
        _currPopingDlayTime = _popUpPlatformBrain.GetpopingDlayTime();
    }
    private void WaitTimer()
    {
        if (_currPopingDlayTime <= 0) { return; }

        _currPopingDlayTime -= Time.deltaTime;
    }

    private void OffsetTimer()
    {
        if (_currOffsetTime <= 0) { return; }

        _currOffsetTime -= Time.deltaTime;
    }


 
}
