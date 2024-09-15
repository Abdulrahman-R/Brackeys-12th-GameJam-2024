using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    public PlayerMovement playerMovement;
    public PlayerJump playerJump;
    public PlayerDeath playerDeath;
    public MovingPlatformDetector movingPlatformDetector;

    public bool _canAct { get;  set; }

    private void Start()
    {
        _canAct = true;
    }
}
