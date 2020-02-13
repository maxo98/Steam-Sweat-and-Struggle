using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	//speed and jumpSpeed 
	[SerializeField]
	private float speed = 30.0f;
	[SerializeField]
	private float jumpForce = 110.0f;
	[SerializeField]
	private float fallSpeed = -70.0f;
	[SerializeField]
	private float dashSpeed = 400f;

	//inputs
	private float HInput;
	private float VInput;
	private bool JInput;
	private float VGazeInput;
	private float HGazeInput;

	//jump condition
	private bool isGrounded = false;
	private float lastTimeGrounded;
	[SerializeField]
	private float fallMultiplier = 20.0f;
	[SerializeField]
	private float lowJumpMultiplier = 40f;
	[SerializeField]
	private float rememberGroundedFor = 0f;


	//hitbox components
	private Rigidbody2D body;
	[SerializeField]
	private Transform groundChecker;
	[SerializeField]
	private float groundCheckerRadius = 1f;
	[SerializeField]
	private LayerMask groundLayer;

	//projectile & dash components
	[SerializeField]
	private GameObject projectilePrefab;
	[SerializeField]
	private float offsetProjectileX = 5.0f;
	[SerializeField]
	private float offsetProjectileY = 6.5f;
	[SerializeField]
	private float fireRate = 1f;
	[SerializeField]
	private float nextFire;
	[SerializeField]
	public float dashRate = 2f;
	[SerializeField]
	public float nextDash;

	//where the character is looking
	private float gazeDirectionAngle;
	private float gazeDirectionY = 0;
	private float gazeDirectionX = 1;




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
		Move();
		Throw();
		CheckIfGrounded();
		BetterJump();
		Jump();
	}

	//movement method
	private void Move()
	{
		HInput = Input.GetAxis("HorizontalPlayerOne");

		//moving the character on the X axis
		body.velocity = new Vector2(HInput * speed, body.velocity.y);

		if (body.velocity.y < fallSpeed)
		{
			body.velocity = new Vector2(body.velocity.x, fallSpeed);
		}
	}

	//Jump method
	private void Jump()
	{
		JInput = Input.GetButton("JumpPlayerOne");
		if (Input.GetButton("JumpPlayerOne") && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
		{
			body.velocity = new Vector2(body.velocity.x, jumpForce);
		}

	}

	private void BetterJump()
	{
		if (body.velocity.y > -4 && !Input.GetButton("JumpPlayerOne"))
		{
			body.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
		else
		{
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
		//throw inputs : 
		//vertical
		float VGazeInput = Input.GetAxis("VerticalGazePlayerOne");
		//horizontal
		float HGazeInput = Input.GetAxis("HorizontalGazePlayerOne");


		float throwDirectionTmp = gazeDirectionX;

		if (!Input.GetButton("HorizontalGazePlayerOne"))
			gazeDirectionX = 0;
		else if (HGazeInput < -0.01)
			gazeDirectionX = -1;
		else if (HGazeInput > 0.01)
			gazeDirectionX = 1;

		if (!Input.GetButton("VerticalGazePlayerOne"))
			gazeDirectionY = 0;
		else if (VGazeInput < -0.01)
			gazeDirectionY = -1;
		else if (VGazeInput > 0.01)
			gazeDirectionY = 1;

		if (gazeDirectionX == 0 && gazeDirectionY == 0)
		{
			gazeDirectionX = throwDirectionTmp;
			if (HInput < -0.01)
				gazeDirectionX = -1;
			else if (HInput > 0.01)
				gazeDirectionX = 1;
		}

		SetGazeAngle();

		if (Input.GetButton("FirePlayerOne") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Shoot();
		}

		if (Input.GetButton("DashPlayerOne") && Time.time > nextDash)
		{
			nextDash = Time.time + dashRate;
			Dash();
		}
	}

	private void Shoot()
	{
		//instanciate the projectile
		GameObject projectile = Instantiate(
						projectilePrefab,
						new Vector3(transform.position.x + gazeDirectionX * offsetProjectileX, transform.position.y + gazeDirectionY * offsetProjectileY, 0),
						projectilePrefab.transform.rotation);

		projectile.GetComponent<Teleportation>().SetMapData(gameObject.GetComponent<Teleportation>().GetMapData());
		ProjectileMovements scriptProjectile = projectile.GetComponent<ProjectileMovements>();
		scriptProjectile.SetDirectionAngle(gazeDirectionAngle);
		Teleportation scriptTel = projectile.GetComponent<Teleportation>();

	}

	/** pas super au point **/
	private void Dash()
	{
		body.velocity = (new Vector2(0, 0));

		//we use cos(angle) and sin(angle) to normalize speed in every direction
		body.AddForce(new Vector2(transform.right.x * dashSpeed * Mathf.Cos(gazeDirectionAngle), transform.up.y * dashSpeed/2 * Mathf.Sin(gazeDirectionAngle)), ForceMode2D.Impulse);
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
}