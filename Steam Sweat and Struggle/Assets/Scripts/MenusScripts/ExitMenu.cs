﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject yes;
    private Image yesImage;
    [SerializeField]
    private GameObject no;
    private Image noImage;

    // Start is called before the first frame update
    void Start()
    {
        yesImage = yes.GetComponent<Image>();
        noImage = no.GetComponent<Image>();

        Focus(yesImage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSubmit()
    {
        if(yesImage.color == Color.grey)
        {
            Application.Quit();
        }
        if(noImage.color == Color.grey)
        {
            SceneManagerWithParameters.Load("menuPrincipal");
        }
    }

    void OnCancel()
    {
        SceneManagerWithParameters.Load("menuPrincipal");
    }

    void OnLeft()
    {
        if (noImage.color == Color.grey)
        {
            UnFocus(noImage);
            Focus(yesImage);
        }
    }

    void OnRight()
    {
        if (yesImage.color == Color.grey)
        {
            UnFocus(yesImage);
            Focus(noImage);
        }
    }

    private void Focus(Image image)
    {
        image.color = Color.grey;
    }

    private void UnFocus(Image image)
    {
        image.color = Color.white;
    }
}
