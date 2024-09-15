using System.Collections;
using UnityEngine;
using System;

public class MovingPlatformDetector : MonoBehaviour
{
    private bool _onPopUp;
    [SerializeField] GameObject _puble;
    [SerializeField] ParticleSystem _particleSystem;
    Rigidbody2D _rb;
    public bool _canParent { get; set; }
    private Vector3 _collisionPos;
    public Action OnBuble;

    private void Start()
    {
        _onPopUp = false;
        _rb = GetComponent<Rigidbody2D>();
        _canParent = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && !_onPopUp && _canParent)
        {
            transform.parent = collision.transform;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }

    private void Update()
    {
        if (_onPopUp)
        {

            transform.position = _collisionPos;
        }

        if (!_canParent)
        {
            transform.parent = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PopUp"))
        {
            if (!_onPopUp)
            {
                _collisionPos = transform.position;
                _onPopUp = true;
                _puble.SetActive(true);
                PlayerController.Instance._canAct = false;
                _particleSystem.Play();
                _rb.gravityScale = 0;
                OnBuble?.Invoke();
            }

           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PopUp"))
        {
            _onPopUp = false;
            _puble.SetActive(false);
            PlayerController.Instance._canAct = true;
            _rb.gravityScale = 1;
        }
  

    }
}
