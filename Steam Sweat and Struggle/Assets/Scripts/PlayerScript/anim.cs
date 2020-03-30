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
        animator.SetFloat("look", control.GetGazeMemory());
        animator.SetInteger("fireGaze", control.GetGazeDirectionX());
        animator.SetFloat("speed", System.Math.Abs(body.velocity.x));
        animator.SetBool("isGrounded", control.GetIsGrounded());
        animator.SetBool("isDashing", control.GetDashing());
        animator.SetBool("isFiring", control.GetFiring());
        Debug.Log(control.GetIsOnLeftWall() + "Left wall");
        Debug.Log(control.GetIsGrounded() + "Grounded");
        animator.SetBool("isOnLeftWall", control.GetIsOnLeftWall());
        animator.SetBool("isOnRightWall", control.GetIsOnRightWall());
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Saut"))
            animator.SetBool("isJumping", true);
        else
            animator.SetBool("isJumping", false);
    }
}