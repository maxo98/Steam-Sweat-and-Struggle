using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField]
    private int selectionPlayerId;
    [SerializeField]
    private GameObject leftArrow;
    [SerializeField]
    private GameObject rightArrow;
    [SerializeField]
    private GameObject confirm;
    [SerializeField]
    private GameObject character1;
    [SerializeField]
    private GameObject character2;
    [SerializeField]
    private GameObject character3;

    private InputManager inputs;

    // Start is called before the first frame update
    void Start()
    {
        selectionPlayerId = Int32.Parse(this.name.Split('r')[1]);
        inputs = GetComponent<InputManager>();
        inputs.SetInputs(1);
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
        character1.SetActive(true);
        character2.SetActive(false);
        character3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Switcher();
        Confirm();
    }

    public void Switcher()
    {
        Debug.Log(inputs.GetHorizontalMovement());
        if (inputs.GetHorizontalMovement() > 0.02)
        {
            Debug.Log("vers la droite");
            if (character1.activeSelf == true)
            {
                character1.SetActive(false);
                character2.SetActive(true);
                character3.SetActive(false);
            }
            else if (character2.activeSelf == true)
            {
                character1.SetActive(false);
                character2.SetActive(false);
                character3.SetActive(true);
            }
            else if (character3.activeSelf == true)
            {
                character1.SetActive(true);
                character2.SetActive(false);
                character3.SetActive(false);
            }
        }
        if(inputs.GetHorizontalMovement() < -0.02)
        {
            Debug.Log("vers la gauche");
            if (character1.activeSelf == true)
            {
                character1.SetActive(false);
                character2.SetActive(false);
                character3.SetActive(true);
            }
            else if (character2.activeSelf == true)
            {
                character1.SetActive(true);
                character2.SetActive(false);
                character3.SetActive(false);
            }
            else if (character3.activeSelf == true)
            {
                character1.SetActive(false);
                character2.SetActive(true);
                character3.SetActive(false);
            }
        }
        Debug.Log("je fais rien");
        
    }

    public void Confirm()
    {
        if (inputs.GetAPressed() && inputs.GetHorizontalMovement() == 0)
        {
            if(leftArrow.activeSelf == true)
            {
                Debug.Log("arrow deactivated");
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);
                confirm.SetActive(false);
            }
            else
            {
                Debug.Log("arrow activated");
                leftArrow.SetActive(true);
                rightArrow.SetActive(true);
                confirm.SetActive(true);
            }
        }
    }

}
