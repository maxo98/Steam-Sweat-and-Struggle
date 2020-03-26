﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //speed and jumpSpeed 
    [SerializeField]
    private float speed = 50.0f;
    [SerializeField]
    private float wallJumpForce = 20.0f;
    [SerializeField]
    private float jumpForce = 150.0f;
    [SerializeField]
    private float fallSpeed = -70.0f;
    [SerializeField]
    private float fastFallSpeed = -140.0f;
    [SerializeField]
    private float dashSpeed = 200.0f;

    //jump condition
    [SerializeField]
    private bool isGrounded = false;
    [SerializeField]
    private bool isOnLeftWall = false;
    [SerializeField]
    private bool isOnRightWall = false;
    private float lastTimeGrounded;
    private float lastTimeOnWall;
    [SerializeField]
    private float fallMultiplier = 20.0f;
    [SerializeField]
    private float lowJumpMultiplier = 40f;
    [SerializeField]
    private float rememberOnWallFor = 0.5f;
    [SerializeField]
    private float rememberGroundedFor = 0f;

    //hitbox components
    public Rigidbody2D body {get; set;}
    [SerializeField]
    private float groundCheckerRadius = 1f;
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform groundChecker;
    [SerializeField]
    private Transform leftWallChecker;
    [SerializeField]
    private Transform rightWallChecker;
    [SerializeField]
    private float wallCheckerRadius = 1f;

	//projectile & dash components
	[SerializeField]
	private GameObject projectilePrefab;
	private float offsetProjectileX = 5.0f;
	private float offsetProjectileY = 6.5f;
    public float projectileSpeed {get; set;} = 100f;
	public int nbShots {get; set;} = 5;
	public int nbRemainingShots {get; set;} = 5;
	public float reload {get; set;} = 3f;
	public float cooldown {get; set;} = 0.2f;
	private float nextFire;
	private float dashRate = 1f;
	private float nextDash;
	private int dashDistance = 8;

	//where the character is looking
	private float gazeDirectionAngle;
	private int gazeDirectionY = 0;
	private int gazeDirectionX = 0;

	private int dashing = 0;
    private bool lastJumped = false;

    private Vector2 movements = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
        //get the components
        body = GetComponent<Rigidbody2D>();
        nextFire = Time.time;
        InitCharacterSpecs();
    }

    protected virtual void InitCharacterSpecs() {

    }
    // Update is called once per frame
    void Update()
    {
		Move();
        CheckIfGrounded();
        CheckIfOnWall();
        BetterJump();
        ResetJump();
    
    }

	protected virtual void Move() {
		if (dashing>0) {
			--dashing;
		} else {
		    //moving the character on the X and Y axis
		    float fSpd = (movements.x < 0) ? fastFallSpeed : fallSpeed;
		    body.velocity = new Vector2(body.velocity.x * 3 / 4 + (movements.x * speed) * 1 / 4, Mathf.Max(body.velocity.y, fSpd));
		}
	}
    //movement methode
    protected virtual void OnMove(InputValue value)
    {
        Debug.Log("Moving");
        movements = value.Get<Vector2>();
    }

    protected virtual void OnJump()
    {
        Debug.Log("Jumping");
        if (!lastJumped)
        {
            if (isGrounded && Time.time - lastTimeGrounded >= rememberGroundedFor)
            {
                body.velocity = new Vector2(body.velocity.x, jumpForce);
            }
            else if (isOnLeftWall && Time.time - lastTimeOnWall >= rememberOnWallFor)
            {

                body.velocity = new Vector2(wallJumpForce * 5, jumpForce);
            }
            else if (isOnRightWall && Time.time - lastTimeOnWall >= rememberOnWallFor)
            {
                body.velocity = new Vector2(-wallJumpForce * 5, jumpForce);
            }
        }
        lastJumped = true;
    }

    protected virtual void OnLook(InputValue value)
    {
        Debug.Log("Looking");
        Vector2 gaze = value.Get<Vector2>();

		if (gaze.x < -0.3)
			gazeDirectionX = -1;
		else if (gaze.x > 0.3)
			gazeDirectionX = 1;
        else
            gazeDirectionX = 0;

		if (gaze.y > 0.3)
			gazeDirectionY = 1;
		else if (gaze.y < -0.3)
			gazeDirectionY = -1;
        else
            gazeDirectionY = 0;
    }

    protected virtual void OnFire()
    {
        Debug.Log("Firing");
        if (Time.time > nextFire)
        {
			AdjustGazeDirection();
			SetGazeAngle();
            //update the time when the player will be able to shoot
            --nbShots;
            if (nbShots<=0) {
                nextFire = Time.time + reload;
                nbShots = nbRemainingShots;
            } else {
                nextFire = Time.time + cooldown;
            }
            //instanciate the projectile
            GameObject projectile = Instantiate(projectilePrefab,
                            new Vector3(transform.position.x + gazeDirectionX * offsetProjectileX, transform.position.y + gazeDirectionY * offsetProjectileY, 0),
                            projectilePrefab.transform.rotation);
            projectile.GetComponent<Teleportation>().SetMapData(gameObject.GetComponent<Teleportation>().GetMapData());
            ProjectileMovements scriptProjectile = projectile.GetComponent<ProjectileMovements>();
            scriptProjectile.SetDirectionAngle(gazeDirectionAngle);
            scriptProjectile.SetSpeed(projectileSpeed);
        }
    }

    protected virtual void OnDash()
    {
        Debug.Log("Dashing");
        if (Time.time > nextDash) 
        {
            //update the time when the player will be able to dash
            nextDash = Time.time + dashRate;
            body.velocity = (new Vector2(0, 0));

            //we use cos(angle) and sin(angle) to normalize speed in every direction
			AdjustGazeDirection();
            body.velocity = new Vector2(gazeDirectionX * dashSpeed, gazeDirectionY * dashSpeed);
			dashing = dashDistance;
        }
    }

    //Jump methode
    private void ResetJump()
    {           
        lastJumped = false;   
    }

    private void BetterJump()
    {
        if (body.velocity.y > -4)
        {
            body.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        } else {
            body.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }

    }
	private void AdjustGazeDirection() {
		if (gazeDirectionX == 0 && gazeDirectionY == 0) {
			if (movements.x < 0)
			    gazeDirectionX = -1;
			else
				gazeDirectionX = 1;
		}
	}
    private void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundLayer);

        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    private void CheckIfOnWall()
    {
        Collider2D collider = Physics2D.OverlapCircle(leftWallChecker.position, wallCheckerRadius, groundLayer);
        if (collider != null)
        {
            isOnLeftWall = true;
        }
        else
        {
            if (isOnLeftWall)
            {
                lastTimeOnWall = Time.time;
            }
            isOnLeftWall = false;
        }
        
        collider = Physics2D.OverlapCircle(rightWallChecker.position, wallCheckerRadius, groundLayer);
        if (collider != null)
        {
            isOnRightWall = true;
        }
        else
        {
            if (isOnRightWall)
            {
                lastTimeOnWall = Time.time;
            }
            isOnRightWall = false;
        }
    }

	//sets the character's gaze direction angle in radians
	public void SetGazeAngle()
	{
		switch (gazeDirectionY)
        {
			case 0:
				switch (gazeDirectionX)
				{
					case 1: case 0:
						gazeDirectionAngle = 0;
						break;
					case -1:
						gazeDirectionAngle = Mathf.PI;
						break;
				}
				break;
			case 1:
				switch (gazeDirectionX)
				{
					case 1:
						gazeDirectionAngle = Mathf.PI / 4;
						break;
					case 0:
						gazeDirectionAngle = Mathf.PI / 2;
						break;
					case -1:
						gazeDirectionAngle = 3 * Mathf.PI / 4;
						break;
				}
				break;
			case -1:
				switch (gazeDirectionX)
				{
					case 1:
						gazeDirectionAngle = 7 * Mathf.PI / 4;
						break;
					case 0:
						gazeDirectionAngle = 3 * Mathf.PI / 2;
						break;
					case -1:
						gazeDirectionAngle = 5 * Mathf.PI / 4;
						break;
				}
				break;
			default:
				break;
		}
	}
}
