using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField]
    private int selectionPlayerId;
    [SerializeField]
    private GameObject selectionPlayer;
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
        if(!inputs.SetInputs(selectionPlayerId))
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Switcher();
        Confirm();
    }

    public void Switcher()
    {
        if (inputs.GetRBPressed())
        {
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
        if(inputs.GetLBPressed())
        {
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
