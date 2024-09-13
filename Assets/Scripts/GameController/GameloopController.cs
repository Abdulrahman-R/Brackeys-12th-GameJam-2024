using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameloopController : MonoBehaviour
{
    public Action OnWining;
    [SerializeField] float _loseDelay;
    [SerializeField] float _winDelay;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.playerDeath.onPlayerDeath += StartLosing;
        OnWining += StartWining;
    }

    private void StartLosing()
    {
        StartCoroutine(LosingCoroutine());
    }

    private void StartWining()
    {
        StartCoroutine(WiningCoroutine());
    }

    IEnumerator LosingCoroutine()
    {

        yield return new WaitForSeconds(_loseDelay);
        GameController.Instance.levelLoader.ReloadLevel();
    }

    IEnumerator WiningCoroutine()
    {

        yield return new WaitForSeconds(_winDelay);
        GameController.Instance.levelLoader.LoadNextLevel();
    }
}
