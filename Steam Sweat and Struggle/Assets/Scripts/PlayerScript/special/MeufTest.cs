using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeufTest : PlayerController
{

    protected override void InitCharacterSpecs() {
        
        nbShots = nbRemainingShots = 6;
        reload = 2.0f;
        cooldown = 0.5f;
        projectileSpeed = 500f;
    }
}
