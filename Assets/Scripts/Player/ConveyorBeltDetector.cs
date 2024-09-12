using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltDetector : MonoBehaviour
{
    private SurfaceEffector2D _surfaceEffector2D;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("belt"))
        {
            SurfaceEffector2D conveyorBelts = collision.gameObject.GetComponent<SurfaceEffector2D>();
            if (conveyorBelts != null)
            {
                if( _surfaceEffector2D == null)
                {
                    _surfaceEffector2D = conveyorBelts;
                    PlayerController.Instance.playerMovement.effectorSpeed = _surfaceEffector2D.speed;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("belt"))
        {
              if (_surfaceEffector2D != null)
              {
                    _surfaceEffector2D = null;
                PlayerController.Instance.playerMovement.effectorSpeed = 0;
              }
           
        }
    }
}
