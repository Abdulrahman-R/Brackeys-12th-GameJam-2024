using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject[] _deathEffect;
    [SerializeField] private float _zOffset;
    private GameObject _currDeathEffect;

    public Action onPlayerDeath;
    // Start is called before the first frame update
    void Start()
    {
        _currDeathEffect = _deathEffect[0];

        onPlayerDeath += SpawnDeathEffect;
        GameController.Instance.modeSwitcher.OnSwitchMode += SwitchDeathEffect;
    }

    private void SpawnDeathEffect()
    {
        if(_currDeathEffect != null)
        {
            GameObject deathEffect = Instantiate(_currDeathEffect, new Vector3(transform.position.x, transform.position.y, _zOffset), Quaternion.identity);
        }
    }

    private void SwitchDeathEffect()
    {
        _currDeathEffect = _deathEffect[1];
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            onPlayerDeath?.Invoke();
            Destroy(gameObject);
        }  
    }
}
