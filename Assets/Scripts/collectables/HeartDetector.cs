using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDetector : MonoBehaviour
{
    [SerializeField] private GameObject _destroyEffect;
    [SerializeField] private float _zOffset;
    [SerializeField] private float _changeModeDelay;
    [SerializeField] private SpriteRenderer GFX;
    private bool _changeHasChanged;

    private void Start()
    {
        _changeHasChanged = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_changeHasChanged) { return; }
            _changeHasChanged = true;
            StartCoroutine(ChangingModeCoroutine());
        }
    }

    IEnumerator ChangingModeCoroutine()
    {
        GFX.enabled = false;
        GameObject deathEffect = Instantiate(_destroyEffect, new Vector3(transform.position.x, transform.position.y, _zOffset), Quaternion.identity);
        yield return new WaitForSeconds(_changeModeDelay);
        GameController.Instance.modeSwitcher.OnSwitchMode?.Invoke();
        Destroy(gameObject);
    }
}
