using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSwitcher : MonoBehaviour
{
 
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

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSubmit()
    {
        if (leftArrow.activeSelf == true)
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
    void OnCancel()
    {

    }

    void OnLeft()
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

    void OnRight()
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
}
