using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
     public DecisionTimeCounter decisionTimeCounter;
     public ModeSwitcher modeSwitcher;
     public CameraShaker cameraShaker;
     public LevelLoader levelLoader;
     public GameloopController gameloopController;
    public AudioManager audioManager;

}
