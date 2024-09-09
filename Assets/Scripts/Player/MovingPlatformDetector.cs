using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformDetector : MonoBehaviour
{
    private Transform _platfrom;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _platfrom = collision.transform;
            transform.parent = _platfrom;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
            _platfrom = null;
        }
    }

}
