using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    private Animator animator;
    private PlayerController control;
    private Rigidbody2D body;
    private float HInput;
    private bool JInput;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("look", 1f);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerController control = gameObject.GetComponent<PlayerController>();
        if (body.velocity.x < 1 && body.velocity.x > -1)
            animator.SetBool("isRunning", false);
        else
            animator.SetBool("isRunning", true);
        animator.SetFloat("look", control.GazeMemory);
        animator.SetInteger("fireGaze", control.GazeDirectionX);
        animator.SetFloat("speed", System.Math.Abs(body.velocity.x));
        animator.SetBool("isGrounded", control.IsGrounded);
        animator.SetBool("isDashing", control.IsDashing);
        animator.SetBool("isFiring", control.IsFiring);
        Debug.Log(control.IsOnLeftWall + "Left wall");
        Debug.Log(control.IsGrounded + "Grounded");
        animator.SetBool("isOnLeftWall", control.IsOnLeftWall);
        animator.SetBool("isOnRightWall", control.IsOnRightWall);
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Saut"))
            animator.SetBool("isJumping", true);
        else
            animator.SetBool("isJumping", false);
    }
}