using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    public PlayerMovement playerMovement;
    public PlayerJump playerJump;

    public bool _canAct { get; private set; }

    private void Start()
    {
        _canAct = true;
    }
}
