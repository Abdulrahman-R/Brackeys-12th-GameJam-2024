using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainDetector : MonoBehaviour
{
    [SerializeField] private GameObject _destroyEffect;
    [SerializeField] private float _zOffset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject deathEffect = Instantiate(_destroyEffect, new Vector3(transform.position.x, transform.position.y, _zOffset), Quaternion.identity);
            GameController.Instance.gameloopController.OnWining?.Invoke();
            Destroy(gameObject);
        }
    }
}
