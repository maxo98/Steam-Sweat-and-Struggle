using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Principal()
    {
        SceneManagerWithParameters.Load("menuPrincipal");
    }

    public void Perso()
    {
        SceneManagerWithParameters.Load("menuPerso");
    }

    public void Map()
    {
        SceneManagerWithParameters.Load("menuMap");
    }

    public void Option()
    {
        SceneManagerWithParameters.Load("menuOption");
    }

    public void Quit()
    {
        SceneManagerWithParameters.Load("menuQuit");
    }

    public void Close()
    {
        Application.Quit();
    }
}


