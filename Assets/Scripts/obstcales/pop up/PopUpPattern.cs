using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PatternMode
{
    AllTogather,
    onlyOne,
}

public class PopUpPattern : MonoBehaviour
{
    [SerializeField] public PatternMode _patternMode;
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

    private void PopUpAllTogather()
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

    private void PopUpOnlyOne()
    {

        _objsToPop[_popCounter].SetActive(true);
        PopOutExceptOne(_popCounter);
        _popCounter++;
        if (_popCounter >= _objsToPop.Length)
        {
            _popCounter = 0;
        }

        Debug.Log("POOOPING");
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
        
        if (_patternMode == PatternMode.AllTogather)
        {
            if (_popingUp)
            {
                if (_currPopingDlayTime <= 0)
                {
                    PopUpAllTogather();
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
        else
        {
            if (_currPopingDlayTime <= 0)
            {
                PopUpOnlyOne();
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



    private void PopOutExceptOne(int index)
    {
        for(int i =0; i < _objsToPop.Length; i++)
        {
            if(i != index)
            {
                _objsToPop[i].SetActive(false);
            }
        }
    }
}
