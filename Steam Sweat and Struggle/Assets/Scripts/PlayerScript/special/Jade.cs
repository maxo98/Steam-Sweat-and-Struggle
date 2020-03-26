using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jade : PlayerController
{

	[SerializeField]
	private GameObject minePrefab;

    [SerializeField]
    private bool isUsedMine = false;

    protected override void InitCharacterSpecs() {
        
        nbShots = nbRemainingShots = 6;
        reload = 2.0f;
        cooldown = 0.5f;
        projectileSpeed = 300f;
    }

    protected override void OnSkill() {
        if (!isUsedMine && isGrounded) {
            isUsedMine = true;
            GameObject projectile = Instantiate(minePrefab,
                            new Vector3(transform.position.x, transform.position.y-3, 0),
                            minePrefab.transform.rotation);
        }
    }
}
