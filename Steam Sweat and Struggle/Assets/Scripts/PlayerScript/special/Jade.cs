using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jade : PlayerController
{
    [SerializeField]
	private GameObject minePrefab;
    private bool hasUsedMine = false;

    protected override void InitCharacterSpecs() {
        
        NbShots = NbRemainingShots = 6;
        Reload = 2.0f;
        Cooldown = 0.5f;
        projectileSpeed = 200f;
    }

    protected override void OnSkill() {
        if (!hasUsedMine && IsGrounded) {
            hasUsedMine = true;
            GameObject projectile = Instantiate(minePrefab,
                            new Vector3(transform.position.x, transform.position.y-2.2f, 0),
                            minePrefab.transform.rotation);
        }
    }
}
