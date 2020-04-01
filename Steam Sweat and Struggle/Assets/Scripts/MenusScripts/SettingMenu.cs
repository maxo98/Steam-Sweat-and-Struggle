using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    private WinCondition number;
    private CharacterName[] numbers;
    private int numberSelected;
    private int index = 0;
    private bool selected;
    // Start is called before the first frame update
    void Start()
    {
        number = GameObject.FindGameObjectWithTag("number").GetComponent<WinCondition>();
       
        numbers = number.Numbers;
        foreach(CharacterName g in numbers)
        {
            if(g == numbers[index])
            {
                g.gameObject.SetActive(true);
            }
            else
            {
                g.gameObject.SetActive(false);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLeft()
    {
        if(!(index == 0))
        {
            numbers[index].gameObject.SetActive(false);
            numbers[index - 1].gameObject.SetActive(true);
            index--;
            numberSelected = Int32.Parse(numbers[index].Name);
        }
        
    }

    void OnRight()
    {
        if (!(index == numbers.Length - 1))
        {
            numbers[index].gameObject.SetActive(false);
            numbers[index + 1].gameObject.SetActive(true);
            index++;
            numberSelected = Int32.Parse(numbers[index].Name);
        }
    }

    void OnSubmit()
    {
        SceneManagerWithParameters.SetGamesToWin(numberSelected);
        selected = true;
    }

    void OnCancel()
    {
        if(!selected)
        {
            SceneManagerWithParameters.SetGamesToWin(numberSelected);
        }
        SceneManagerWithParameters.Load("menuPrincipal");
    }
}
