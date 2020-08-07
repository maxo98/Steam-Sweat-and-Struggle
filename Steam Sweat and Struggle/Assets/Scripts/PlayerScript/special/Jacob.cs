using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jacob : PlayerController
{
    [SerializeField]
	private GameObject exploPrefab;
    private bool hasUsedExplosion = false;

    protected override void InitCharacterSpecs() {
        
        NbShots = NbRemainingShots = 1;
        Reload = 3.0f;
        Cooldown = 2.0f;
        projectileSpeed = 100f;
    }

    protected override void OnSkill() {
        if (!hasUsedExplosion) {
            hasUsedExplosion = true;
            GameObject projectile = Instantiate(exploPrefab,
                            new Vector3(transform.position.x, transform.position.y, 0),
                            exploPrefab.transform.rotation);
            projectile.SendMessage("SetParent", gameObject);
        }
    }
}
