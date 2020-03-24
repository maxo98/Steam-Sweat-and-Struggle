using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private int idController;

    //axis names
    private string horizontalMovementAxis;
    private string verticalMovementAxis;
    private string horizontalLookAxis;
    private string verticalLookAxis;

    //action buttons
    private string A;
    private string B;
    private string LT;
    private string RT;
    private string LB;
    private string RB;

    //controllers 
    string joystick;

    public bool SetInputs(int number)
    {
        idController = number;
        if (number > Input.GetJoystickNames().Length)
        {
            return false;
        }
        else 
        { 
            joystick = Input.GetJoystickNames()[idController - 1];
            horizontalMovementAxis = "HorizontalMovement" + idController;
            verticalMovementAxis = "VerticalMovement" + idController;
            horizontalLookAxis = "HorizontalLook" + idController;
            verticalLookAxis = "VerticalLook" + idController;
            foreach(string s in Input.GetJoystickNames()) { Debug.Log(s); }
            A = "A" + idController;
            B = "B" + idController;
            LT = "LT" + idController;
            RT = "RT" + idController;
            LB = "LB" + idController;
            RB = "RB" + idController;

            return true;

            
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getIdController()
    {
        return idController;
    }

    public string getJoystick()
    {
        return joystick;
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

    public bool GetAPressed()
    {
        return Input.GetButtonDown(A);
    }

    public bool GetA()
    {
        return Input.GetButton(A);
    }

    public bool GetBPressed()
    {
        return Input.GetButtonDown(B);
    }

    public bool GetB()
    {
        return Input.GetButton(B);
    }

    public bool GetLT()
    {
        return Input.GetAxis(LT) > 0.1;
    }

    public bool GetRT()
    {
        return Input.GetAxis(RT) > 0.1;
    }

    public bool GetLBPressed()
    {
        return Input.GetButtonDown(LB);
    }

    public bool GetRBPressed()
    {
        return Input.GetButtonDown(RB);
    }

}
