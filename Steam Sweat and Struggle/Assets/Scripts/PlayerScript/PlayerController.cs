using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private int playerNumber;

    //speed and jumpSpeed 
    [SerializeField]
    private float speed = 30.0f;
    [SerializeField]
    private float jumpForce = 110.0f;
    [SerializeField]
    private float fallSpeed = -70.0f;
    [SerializeField]
    private float offsetProjectile = 5.0f;

    //inputs
    private PlayerInput inputs;

    //jump condition
    private bool isGrounded = false;
    private float lastTimeGrounded;
    [SerializeField]
    private float fallMultiplier = 20.0f;
    [SerializeField]
    private float lowJumpMultiplier = 40f;
    [SerializeField]
    private float rememberGroundedFor = 0f;
    [SerializeField]
    private float fireRate = 1f;

    [SerializeField]
    private float nextFire;

    //hitbox components
    private Rigidbody2D body;
    [SerializeField]
    private Transform groundChecker;
    [SerializeField]
    private float groundCheckerRadius = 1f;
    [SerializeField]
    private LayerMask groundLayer;

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
        inputs = GetComponent<PlayerInput>();
        inputs.SetInputs(1);
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		if (inputs.GetHorizontalMovement() < -0.01)
			look = -1;
		if (inputs.GetHorizontalMovement() > 0.01)
			look = 1;
        Move();
        Throw();
        CheckIfGrounded();
        BetterJump();
        Jump();
        
        
    }

    //movement methode
    private void Move()
    {
        //moving the character on the X axis
        body.velocity = new Vector2(inputs.GetHorizontalMovement() * speed, body.velocity.y);

        if (body.velocity.y < fallSpeed)
        {
            body.velocity = new Vector2(body.velocity.x,fallSpeed);
        }
    }

    //Jump methode
    private void Jump()
    {
        JInput = Input.GetButton("Jump1");
        if (Input.GetButton("Jump1") && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
        
    }

    private void BetterJump()
    {
        if (body.velocity.y > -4 && !Input.GetButton("Jump1"))
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

    private void Throw()
	{

		if (HInput < -0.01)
			throwDirectionX = -1;
		if (HInput > 0.01)
			throwDirectionX = 1;
        
		if (Input.GetButton("Fire1") && Time.time>nextFire)
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
