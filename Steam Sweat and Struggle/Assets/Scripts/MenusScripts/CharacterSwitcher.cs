using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSwitcher : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject confirm;

    public GameObject character1;
    public GameObject character2;
    public GameObject character3;

    // Start is called before the first frame update
    void Start()
    {
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
        character1.SetActive(true);
        character2.SetActive(false);
        character3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switcher()
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

    public void Confirm()
    {
        if (leftArrow.activeSelf == true)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
        }
        else
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
        }
    }

}
