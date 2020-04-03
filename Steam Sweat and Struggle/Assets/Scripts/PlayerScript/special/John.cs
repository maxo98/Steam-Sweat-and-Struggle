using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class John : PlayerController
{
    private bool hasUsedFreeze = false;

    protected override void InitCharacterSpecs() {
        
        NbShots = NbRemainingShots = 1;
        Reload = 4.0f;
        Cooldown = 2.0f;
        projectileSpeed = 200f;
    }

    protected override void OnSkill() {
        if (!hasUsedFreeze) {
            hasUsedFreeze = true;
        }
    }

}
