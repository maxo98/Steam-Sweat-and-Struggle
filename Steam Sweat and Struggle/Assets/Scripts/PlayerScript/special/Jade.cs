using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jade : PlayerController
{
	private GameObject minePrefab;
    private bool hasUsedMine = false;

    protected override void InitCharacterSpecs() {
        
        nbShots = nbRemainingShots = 6;
        reload = 2.0f;
        cooldown = 0.5f;
        projectileSpeed = 200f;
    }

    protected override void OnSkill() {
        if (!hasUsedMine && isGrounded) {
            hasUsedMine = true;
            GameObject projectile = Instantiate(minePrefab,
                            new Vector3(transform.position.x, transform.position.y-3, 0),
                            minePrefab.transform.rotation);
        }
    }
}
