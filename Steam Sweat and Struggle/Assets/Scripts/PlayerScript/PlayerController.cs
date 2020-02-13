using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float offsetProjectile = 5.0f;

    //inputs
    private float HInput;
    private bool JInput;

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
    [SerializeField]
    private float fireRate = 1f;

    [SerializeField]
    private float nextFire;

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

	[SerializeField]
	private GameObject projectilePrefab;
	private float look = 1;


	private float throwDirectionX = 0;
	private float throwDirectionY = 0;

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
		if (HInput < -0.01)
			look = -1;
		if (HInput > 0.01)
			look = 1;
        Move();
        Throw();
        CheckIfGrounded();
        CheckIfOnWall();
        BetterJump();
        Jump();
    
    }

    //movement methode
    private void Move()
    {
        HInput = Input.GetAxis("HorizontalPlayerOne");

        //moving the character on the X axis
        float fSpd = (Input.GetAxis("VerticalPlayerOne")<0)?fastFallSpeed:fallSpeed;
        body.velocity = new Vector2(body.velocity.x*3/4 + (HInput * speed)*1/4, Mathf.Max(body.velocity.y, fSpd));

    }

    //Jump methode
    private void Jump()
    {
        JInput = Input.GetButton("JumpPlayerOne");
        if (JInput) {
            if (isGrounded && Time.time - lastTimeGrounded >= rememberGroundedFor)
            {
                body.velocity = new Vector2(body.velocity.x, jumpForce);
            }else if (isOnLeftWall && Time.time - lastTimeOnWall >= rememberOnWallFor)
            {
                
                body.velocity = new Vector2(wallJumpForce*5, jumpForce);
            }else if (isOnRightWall && Time.time - lastTimeOnWall >= rememberOnWallFor)
            {
                body.velocity = new Vector2(-wallJumpForce*5, jumpForce);
            }
        }
        
    }

    private void BetterJump()
    {
        if (body.velocity.y > -4 && !Input.GetButton("JumpPlayerOne"))
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

    private void Throw()
	{

		if (HInput < -0.01)
			throwDirectionX = -1;
		if (HInput > 0.01)
			throwDirectionX = 1;
        
		if (Input.GetButton("FirePlayerOne") && Time.time>nextFire)
		{
			
            nextFire = Time.time + fireRate;
            Shoot();
		}
	}
    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x + throwDirectionX * offsetProjectile, transform.position.y, 0), projectilePrefab.transform.rotation);
        projectile.GetComponent<Teleportation>().SetMapData(gameObject.GetComponent<Teleportation>().GetMapData());
        ProjectileMovements scriptProjectile = projectile.GetComponent<ProjectileMovements>();
        scriptProjectile.setThrowDirection(throwDirectionX);
        Teleportation scriptTel = projectile.GetComponent<Teleportation>();
    }
}
