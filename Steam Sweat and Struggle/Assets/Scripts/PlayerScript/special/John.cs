using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class John : PlayerController
{
    private float shotFreezeTime = 4.0f;
    private bool freezed = false;
    private float endShotFreeze = 0;
    private bool hasUsedFreeze = false;

    protected override void InitCharacterSpecs() {
        
        NbShots = NbRemainingShots = 1;
        Reload = 4.0f;
        Cooldown = 2.0f;
        projectileSpeed = 200f;
    }

    protected override void Update() {
        base.Update();
        if (endShotFreeze<Time.time && freezed) {
            freezed = false;
            gameObject.GetComponent<Teleportation>().GetMapData().SendMessage("SetShotSpeed", 1.0f);
        }
    }
    protected override void OnSkill() {
        if (!hasUsedFreeze) {
            hasUsedFreeze = true;
            freezed = true;
            endShotFreeze = Time.time + shotFreezeTime;
            gameObject.GetComponent<Teleportation>().GetMapData().SendMessage("SetShotSpeed", 0.2f);
        }
    }

}
