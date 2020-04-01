using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Judith : PlayerController
{
    [SerializeField]
	private GameObject laserPrefab;
    private bool hasUsedLaser = false;

    private float laserSpeed = 50f;

    protected override void InitCharacterSpecs() {
        
        NbShots = NbRemainingShots = 6;
        Reload = 4.0f;
        Cooldown = 0.25f;
        projectileSpeed = 200f;
    }

    protected override void OnSkill() {
        if (!hasUsedLaser) {
            hasUsedLaser = true;

            AdjustGazeDirection();
			SetGazeAngle();
            
            if (GazeDirectionX == 0 && GazeDirectionY == 0)
                GazeDirectionX = GazeMemory;
            
            //instanciate the projectile
            GameObject projectile = Instantiate(laserPrefab,
                            new Vector3(transform.position.x + GazeDirectionX * offsetProjectileX, transform.position.y + GazeDirectionY * offsetProjectileY, 0),
                            laserPrefab.transform.rotation);
            projectile.GetComponent<Teleportation>().SetMapData(gameObject.GetComponent<Teleportation>().GetMapData());
            LaserAction scriptProjectile = projectile.GetComponent<LaserAction>();
            scriptProjectile.SetDirectionAngle(gazeDirectionAngle);
            scriptProjectile.SetSpeed(laserSpeed);
        }
    }

}
