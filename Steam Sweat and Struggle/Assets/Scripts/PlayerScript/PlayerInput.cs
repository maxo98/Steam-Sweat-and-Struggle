using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private int playerNumber;

    //axis names
    private string horizontalMovementAxis;
    private string verticalMovementAxis;
    private string horizontalLookAxis;
    private string verticalLookAxis;

    //action buttons
    private string jump;
    private string fire;
    private string dash;

    public void SetInputs(int number)
    {
        playerNumber = number;
        horizontalMovementAxis = "HorizontalMovement" + playerNumber;
        verticalMovementAxis = "VerticalMovement" + playerNumber;
        horizontalLookAxis = "HorizontalLook" + playerNumber;
        verticalLookAxis = "VerticalLook" + playerNumber;

        jump = "Jump" + playerNumber;
        fire = "Fire" + playerNumber;
        dash = "Dash" + playerNumber;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetHorizontalMovement()
    {
        return Input.GetAxis(horizontalMovementAxis);
    }

    public float GetVerticalMovement()
    {
        return Input.GetAxis(verticalMovementAxis);
    }

    public float GetHorizontalLook()
    {
        return Input.GetAxis(horizontalLookAxis);
    }

    public float GetVerticalLook()
    {
        return Input.GetAxis(verticalLookAxis);
    }

    public bool GetJumpPressed()
    {
        return Input.GetButton(jump);
    }

    public bool GetJump()
    {
        return Input.GetButton(jump);
    }



    public bool GetFirePressed()
    {
        return Input.GetButtonDown(fire);
    }

    public bool GetFire()
    {
        return Input.GetButton(fire);
    }

    public bool GetDash()
    {
        return Input.GetButton(dash);
    }



}
