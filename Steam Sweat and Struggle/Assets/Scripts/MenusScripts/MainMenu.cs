using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject versus;
    private Image versusImage;
    [SerializeField]
    private GameObject option;
    private Image optionImage;
    [SerializeField]
    private GameObject online;
    private Image onlineImage;
    [SerializeField]
    private GameObject quit;
    private Image quitImage;
    

    // Start is called before the first frame update
    void Start()
    {
        versusImage = versus.GetComponent<Image>();
        optionImage = option.GetComponent<Image>();
        onlineImage = online.GetComponent<Image>();
        quitImage = quit.GetComponent<Image>();

        Focus(versusImage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Focus(Image image)
    {
        image.color = Color.grey;
    }

    private void UnFocus(Image image)
    {
        image.color = Color.white;
    }

    void OnSubmit()
    {
        if (versusImage.color == Color.grey)
        {
            SceneManagerWithParameters.Load("menuMap");
        }
        if (optionImage.color == Color.grey)
        {
            SceneManagerWithParameters.Load("menuOption");
        }
        if (quitImage.color == Color.grey)
        {
            SceneManagerWithParameters.Load("menuQuit");
        }
    }

    void OnCancel()
    {
        SceneManagerWithParameters.Load("menuQuit");
    }

    void OnLeft()
    {
        if (optionImage.color == Color.grey)
        {
            UnFocus(optionImage);
            Focus(versusImage);
        }
        if (quitImage.color == Color.grey)
        {
            UnFocus(quitImage);
            Focus(onlineImage);
        }
    }

    void OnRight()
    {
        if (versusImage.color == Color.grey)
        {
            UnFocus(versusImage);
            Focus(optionImage);
        }
        if (onlineImage.color == Color.grey)
        {
            UnFocus(onlineImage);
            Focus(quitImage);
        }
    }

    void OnUp()
    {
        if (onlineImage.color == Color.grey)
        {
            UnFocus(onlineImage);
            Focus(versusImage);
        }
        if (quitImage.color == Color.grey)
        {
            UnFocus(quitImage);
            Focus(optionImage);
        }
    }

    void OnDown()
    {
        if (versusImage.color == Color.grey)
        {
            UnFocus(versusImage);
            Focus(onlineImage);
        }
        if (optionImage.color == Color.grey)
        {
            UnFocus(optionImage);
            Focus(quitImage);
        }

    }
}

