using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTwoController : MonoBehaviour
{
    
    //speed and jumpSpeed 
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float jumpForce = 10.0f;

    //inputs
    private float HInput;

    //jump condition
    private bool isGrounded = false;
    private float fallMultiplier = 1.0f;
    private float lowJumpMultiplier = 2.5f;
    private float rememberGroundedFor = 0.2f;
    private float lastTimeGrounded;

    //hitbox components
    private Rigidbody2D body;
    [SerializeField]
    private Transform groundChecker;
    [SerializeField]
    private float groundCheckerRadius = 0.5f;
    [SerializeField]
    private LayerMask groundLayer;

    

    // Start is called before the first frame update
    void Start()
    {
        //get the components
        body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckIfGrounded();
        BetterJump();
        Jump();
        
        
    }

    //movement methode
    private void Move()
    {
        HInput = Input.GetAxis("HorizontalPlayerTwo");

        //moving the character on the X axis
        body.velocity = new Vector2(HInput * speed, body.velocity.y);
    }

    //Jump methode
    private void Jump()
    {

        if (Input.GetButton("JumpPlayerTwo") && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
        }
        
    }

    private void BetterJump()
    {
        if(body.velocity.y < 0)
        {
            body.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (body.velocity.y > 0 && !Input.GetButton("JumpPlayerTwo"))
        {
            body.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
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



}
