﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    private Animator animator;
    private PlayerController control;
    private Rigidbody2D body;
    private float HInput;
    private bool JInput;

    [SerializeField]
    private Transform groundChecker;
    [SerializeField]
    private float groundCheckerRadius = 1f;
    [SerializeField]
    private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("look", 1f);
        PlayerController control = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController control = gameObject.GetComponent<PlayerController>();
        HInput = Input.GetAxis("HorizontalPlayerOne");
        JInput = Input.GetButton("JumpPlayerOne");
        Debug.Log(HInput);
        if (HInput < 0.001 && HInput > -0.001)
            animator.SetBool("isRunning", false);
        else
            animator.SetBool("isRunning", true);
        if (HInput < -0.01)
            animator.SetFloat("look", -1f);
        if (HInput > 0.01)
            animator.SetFloat("look", 1f);
        animator.SetBool("isJumping", JInput);
        animator.SetFloat("vVelocity", body.velocity.y);
        animator.SetFloat("hVelocity", body.velocity.x);
        animator.SetFloat("speed", System.Math.Abs(body.velocity.x));
        animator.SetBool("isGrounded", control.GetIsGrounded());

        /*Collider2D collider = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundLayer);

        if (collider != null)
        {
            animator.SetBool("isGrounded", true);
        }
        else
        {
            animator.SetBool("isGrounded", false);
        }*/
    }
}