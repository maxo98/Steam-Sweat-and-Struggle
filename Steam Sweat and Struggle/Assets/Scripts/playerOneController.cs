﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerOneController : MonoBehaviour
{

    [SerializeField]
    private Camera camera;

    //speed and jumpSpeed 
    [SerializeField]
    private float speed = 30.0f;
    [SerializeField]
    private float jumpForce = 110.0f;

    //inputs
    private float HInput;
    private bool JInput;

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
	private float directionRegard = 1;


	private float dirLancerX = 0;
	private float dirLancerY = 0;

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
			directionRegard = -1;
		if (HInput > 0.01)
			directionRegard = 1;
        Move();
        Lancer();
        CheckIfGrounded();
        BetterJump();
        Jump();
        
        
    }

    //movement methode
    private void Move()
    {
        HInput = Input.GetAxis("HorizontalPlayerOne");

        //moving the character on the X axis
        body.velocity = new Vector2(HInput * speed, body.velocity.y);
    }

    //Jump methode
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

    private void Lancer()
	{

		if (HInput < -0.01)
			dirLancerX = -1;
		if (HInput > 0.01)
			dirLancerX = 1;
        
		if (Input.GetButton("FirePlayerOne") && Time.time>nextFire)
		{
			
            nextFire = Time.time + fireRate;
            Shoot();
		}
	}
    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, new Vector3(transform.position.x + dirLancerX * 5, transform.position.y, 0), projectilePrefab.transform.rotation);
        DeplacementProjectile scriptProjectile = projectile.GetComponent<DeplacementProjectile>();
        scriptProjectile.SetLancer(dirLancerX);
        teleportation scriptTel = projectile.GetComponent<teleportation>();
        scriptTel.SetCamera(camera);
    }
}
