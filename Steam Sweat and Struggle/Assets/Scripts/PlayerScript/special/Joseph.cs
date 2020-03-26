using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Joseph : PlayerController
{

    protected override void InitCharacterSpecs() {
        
        nbShots = nbRemainingShots = 4;
        reload = 3.0f;
        cooldown = 1.0f;
        projectileSpeed = 100f;
    }
}
