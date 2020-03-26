using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Joseph : PlayerController
{

	private float invincibilityDelay = 2.0f;
	private float timeInvicible;
    private bool hasUsedInvincibility = false;

    protected override void InitCharacterSpecs() {
        
        nbShots = nbRemainingShots = 4;
        reload = 3.0f;
        cooldown = 1.0f;
        projectileSpeed = 100f;
        timeInvicible = Time.time;

    }

    protected override void OnSkill() {
        if (!hasUsedInvincibility) {
            timeInvicible = Time.time+invincibilityDelay;
        }
    }

    protected override void OnFire() {
        if (Time.time>timeInvicible) {
            base.OnFire();
        } else {
            Debug.Log("Can't fire when invincible.");
        }
    }

    protected override void OnDie() {
        if (Time.time>timeInvicible) {
            timeInvicible = Time.time+invincibilityDelay;
            Debug.Log("YOU DIED");
            Destroy(gameObject);
        } else {
            Debug.Log("INVINCIBLE");
        }
    }
}
