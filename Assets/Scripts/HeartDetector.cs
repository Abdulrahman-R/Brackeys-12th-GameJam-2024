using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDetector : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.Instance.modeSwitcher.OnSwitchMode?.Invoke();
            Destroy(gameObject);
        }
    }
}
