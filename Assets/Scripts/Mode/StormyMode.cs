using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StormyMode : MonoBehaviour
{
    [Header("Stormy Mode Configs")]
    [SerializeField] protected GameObject[] _objsToHide;
    [SerializeField] protected GameObject[] _objsToShow;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void OnStromyMode()
    {
        if(_objsToHide.Length > 0)
        {
            for (int i = 0; i < _objsToHide.Length; i++)
            {
                _objsToHide[i].SetActive(false);
            }
        }
      
        if(_objsToShow.Length > 0)
        {
            for (int i = 0; i < _objsToShow.Length; i++)
            {
                _objsToShow[i].SetActive(true);
            }
        }
    }
}
