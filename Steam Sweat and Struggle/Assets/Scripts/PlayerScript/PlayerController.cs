using System.Collections;
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
    private float jumpForce = 110.0f;
    [SerializeField]
    private float fallSpeed = -70.0f;
    [SerializeField]
    private float fastFallSpeed = -140.0f;
    [SerializeField]
    private float dashSpeed = 500;

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
    private float rememberGroundedFor = 0.5f;

    //hitbox components
    private Rigidbody2D body;
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
    private float wallCheckerRadius = 2f;

	//projectile & dash components
	[SerializeField]
	private GameObject projectilePrefab;
	[SerializeField]
	private float offsetProjectileX = 5.0f;
	[SerializeField]
	private float offsetProjectileY = 6.5f;
	[SerializeField]
	private float fireRate = 0.2f;
    [SerializeField]
	private float nextFire;
	[SerializeField]
	private float dashRate = 2f;
	[SerializeField]
	private float nextDash;
	private float dashDistance = 15f;

	//where the character is looking
	private float gazeDirectionAngle;
	private int gazeDirectionY = 0;
	private int gazeDirectionX = 1;
    private int gazeMemory = 1;

    private int timerFiring = 0;
    private bool firing = false;
    private int timerDashing = 0;
    private bool dashing = false;
    bool lastJumped = false;

    Vector2 movements = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
        //get the components
        body = GetComponent<Rigidbody2D>();
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //moving the character on the X axis
        float fSpd = (movements.x < 0) ? fastFallSpeed : fallSpeed;
        if (movements.x < -0.1)
            gazeMemory = -1;
        else if (movements.x > 0.1)
            gazeMemory = 1;
        body.velocity = new Vector2(body.velocity.x * 3 / 4 + (movements.x * speed) * 1 / 4, Mathf.Max(body.velocity.y, fSpd));

        if (timerDashing == 0)
            dashing = false;
        else
            --timerDashing;

        if (timerFiring == 0)
            firing = false;
        else
            --timerFiring;

        CheckIfGrounded();
        CheckIfOnWall();
        BetterJump();
        ResetJump();
    
    }

    //movement methode
    void OnMove(InputValue value)
    {
        Debug.Log("Moving");
        movements = value.Get<Vector2>();

        //player gaze by default
        if (gazeDirectionX == 0 && gazeDirectionY == 0)
        {
            if (movements.x < -0.1)
                gazeDirectionX = -1;
            else if (movements.x > 0.1)
                gazeDirectionX = 1;
            else if (gazeDirectionX == 0)
                gazeDirectionX = 1;
        }

        SetGazeAngle();

    }

    void OnJump()
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

    void OnLook(InputValue value)
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

		SetGazeAngle();
    }

    void OnFire()
    {
        Debug.Log("Firing");
        if (Time.time > nextFire)
        {
            firing = true;
            timerFiring = 5; 
            //update the time when the player will be able to shoot
            nextFire = Time.time + fireRate;
            //instanciate the projectile
            GameObject projectile = Instantiate(projectilePrefab,
                            new Vector3(transform.position.x + gazeDirectionX * offsetProjectileX, transform.position.y + gazeDirectionY * offsetProjectileY, 0),
                            projectilePrefab.transform.rotation);
            projectile.GetComponent<Teleportation>().SetMapData(gameObject.GetComponent<Teleportation>().GetMapData());
            ProjectileMovements scriptProjectile = projectile.GetComponent<ProjectileMovements>();
            scriptProjectile.SetDirectionAngle(gazeDirectionAngle);
        }
    }

    void OnDash()
    {
        Debug.Log("Dashing");
        if (Time.time > nextDash) 
        {
            //update the time when the player will be able to dash
            nextDash = Time.time + dashRate;
            body.velocity = (new Vector2(0, 0));

            //we use cos(angle) and sin(angle) to normalize speed in every direction
            //body.AddForce(new Vector2(transform.right.x * dashSpeed * Mathf.Cos(gazeDirectionAngle), transform.up.y * dashSpeed/2 * Mathf.Sin(gazeDirectionAngle)), ForceMode2D.Impulse);

            //transform.position = Vector2.Lerp(
            //				new Vector2(transform.position.x, transform.position.y),
            //				new Vector2(transform.position.x + gazeDirectionX * dashDistance, transform.position.y + gazeDirectionY * dashDistance),
            //				dashSpeed * Time.deltaTime);
            dashing = true;
            timerDashing = 5;
            body.velocity = new Vector2(gazeDirectionX * dashSpeed, gazeDirectionY * dashSpeed);

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
					case 1:
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

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public int GetGazeDirectionX()
    {
        return gazeMemory;
    }

    public bool GetDashing()
    {
        return dashing;
    }

    public bool GetFiring()
    {
        return firing;
    }
    public bool GetIsOnRightWall()
    {
        return isOnRightWall;
    }

    public bool GetIsOnLeftWall()
    {
        return isOnLeftWall;
    }
}

