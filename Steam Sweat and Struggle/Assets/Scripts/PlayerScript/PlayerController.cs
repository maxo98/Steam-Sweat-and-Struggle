using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //speed and jumpSpeed 
    
    private float speed = 50.0f;
    private float wallJumpForce = 50.0f;
    private float jumpForce = 100.0f;
    private float fallSpeed = -70.0f;
    private float fastFallSpeed = -100.0f;
    private float dashSpeed = 200.0f;

    //jump condition
    
    public bool IsGrounded {get; set; } = false;
    
    public bool IsOnLeftWall {get; set;} = false;
    
    public bool IsOnRightWall {get; set;} = false;
    private float lastTimeGrounded;
    private float lastTimeOnWall;
    private float fallMultiplier = 20.0f;
    private float lowJumpMultiplier = 40f;
    private float rememberOnWallFor = 0.5f;
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
	public float offsetProjectileX {get; set;} = 5.0f;
	public float offsetProjectileY {get; set;} = 6.5f;
    public float projectileSpeed {get; set;} = 100f;
	public int NbShots {get; set;} = 5;
	public int NbRemainingShots {get; set;} = 5;
	public float Reload {get; set;} = 3f;
	public float Cooldown {get; set;} = 0.2f;
	private float nextFire;
	private float dashRate = 1f;
	private float nextDash;
	private int dashDistance = 8;

	//where the character is looking
	public float gazeDirectionAngle { get; set; }
	public int GazeDirectionY { get; set; } = 0;
	public int GazeDirectionX { get; set; } = 0;

	private int dashing = 0;
    private bool lastJumped = false;
    private bool jumping = false;

    private Vector2 movements = new Vector2(0,0);

    public int GazeMemory {get; set;} = 1;

    private int timerFiring = 0;
    public bool IsFiring {get; set;} = false;
    private int timerDashing = 0;
    public bool IsDashing {get; set;} = false;


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
        actionChecker();
        CheckIfGrounded();
        CheckIfOnWall();
        BetterJump();
        ResetJump();

    }

    protected virtual void actionChecker() {
        if (movements.x < -0.1)
            GazeMemory = -1;
        else if (movements.x > 0.1)
            GazeMemory = 1;

        if (timerDashing == 0)
            IsDashing = false;
        else
            --timerDashing;

        if (timerFiring == 0)
            IsFiring = false;
        else
            --timerFiring;
    }

	protected virtual void Move() {
		if (dashing>0) {
			--dashing;
		} else {
		    //moving the character on the X and Y axis
		    var fSpd = (movements.y < 0) ? fastFallSpeed : fallSpeed;
		    body.velocity = new Vector2(body.velocity.x * 3 / 4 + (movements.x * speed) * 1 / 4, Mathf.Max(body.velocity.y, fSpd));
		}
	}

    protected virtual void OnSkill() {

    }

    //movement method
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
            
            if (IsGrounded && Time.time - lastTimeGrounded >= rememberGroundedFor)
            {
                body.velocity = new Vector2(body.velocity.x, jumpForce);
                jumping = true;
            }
            else if (IsOnLeftWall && Time.time - lastTimeOnWall >= rememberOnWallFor)
            {

                body.velocity = new Vector2(wallJumpForce * 5, jumpForce);
                jumping = true;
            }
            else if (IsOnRightWall && Time.time - lastTimeOnWall >= rememberOnWallFor)
            {
                body.velocity = new Vector2(-wallJumpForce * 5, jumpForce);
                jumping = true;
            }
        }
        lastJumped = true;
    }

    void OnStopJump()
    {
        jumping = false;
    }

    protected virtual void OnLook(InputValue value)
    {
        Debug.Log("Looking");
        Vector2 gaze = value.Get<Vector2>();

        if (gaze.x < -0.3)
            GazeDirectionX = -1;
        else if (gaze.x > 0.3)
            GazeDirectionX = 1;
        else
            GazeDirectionX = 0;

        if (gaze.y > 0.3)
            GazeDirectionY = 1;
        else if (gaze.y < -0.3)
            GazeDirectionY = -1;
        else
            GazeDirectionY = 0;
    }

    protected virtual void OnFire()
    {
        Debug.Log("Firing");
        if (Time.time > nextFire)
        {
			AdjustGazeDirection();
			SetGazeAngle();
            //update the time when the player will be able to shoot
            --NbShots;
            if (NbShots<=0) {
                nextFire = Time.time + Reload;
                NbShots = NbRemainingShots;
            } else {
                nextFire = Time.time + Cooldown;
            }
            IsFiring = true;
            timerFiring = 15;
            
            if (GazeDirectionX == 0 && GazeDirectionY == 0)
                GazeDirectionX = GazeMemory;

            //instanciate the projectile
            GameObject projectile = Instantiate(projectilePrefab,
                            new Vector3(transform.position.x + GazeDirectionX * offsetProjectileX, transform.position.y + GazeDirectionY * offsetProjectileY, 0),
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
            body.velocity = new Vector2(GazeDirectionX * dashSpeed, GazeDirectionY * dashSpeed);
			dashing = dashDistance;
            IsDashing = true;
            timerDashing = 5;
        }
    }

    //Jump methode
    private void ResetJump()
    {
        lastJumped = false;
    }

    private void BetterJump()
    {
        if (body.velocity.y > -4 && !jumping)
        {
            body.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else
        {
            body.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }

    }

	protected void AdjustGazeDirection() {
		if (GazeDirectionX == 0 && GazeDirectionY == 0) {
			if (movements.x < 0)
			    GazeDirectionX = -1;
			else
				GazeDirectionX = 1;
		}
	}
    private void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundLayer);

        if (collider != null)
        {
            IsGrounded = true;
        }
        else
        {
            if (IsGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            IsGrounded = false;
        }
    }

    private void CheckIfOnWall()
    {
        Collider2D collider = Physics2D.OverlapCircle(leftWallChecker.position, wallCheckerRadius, groundLayer);
        if (collider != null)
        {
            IsOnLeftWall = true;
        }
        else
        {
            if (IsOnLeftWall)
            {
                lastTimeOnWall = Time.time;
            }
            IsOnLeftWall = false;
        }

        collider = Physics2D.OverlapCircle(rightWallChecker.position, wallCheckerRadius, groundLayer);
        if (collider != null)
        {
            IsOnRightWall = true;
        }
        else
        {
            if (IsOnRightWall)
            {
                lastTimeOnWall = Time.time;
            }
            IsOnRightWall = false;
        }
    }

    //sets the character's gaze direction angle in radians
    protected void SetGazeAngle()
    {
        switch (GazeDirectionY)
        {
			case 0:
				switch (GazeDirectionX)
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
				switch (GazeDirectionX)
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
				switch (GazeDirectionX)
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
    protected virtual void OnDie() {
        Debug.Log("YOU DIED");
        gameObject.GetComponent<Teleportation>().GetMapData().SendMessage("OnDeath", gameObject);


    }
}
