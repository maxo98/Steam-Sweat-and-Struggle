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

    public void SetInputs(int number)
    {
        idController = number;
        horizontalMovementAxis = "HorizontalMovement" + idController;
        verticalMovementAxis = "VerticalMovement" + idController;
        horizontalLookAxis = "HorizontalLook" + idController;
        verticalLookAxis = "VerticalLook" + idController;

        A = "A" + idController;
        B = "B" + idController;
        LT = "LT" + idController;
        RT = "RT" + idController;
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
        return Input.GetButton(LT);
    }

    public bool GetRT()
    {
        return Input.GetButton(RT);
    }



}
