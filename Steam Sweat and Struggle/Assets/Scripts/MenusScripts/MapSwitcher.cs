using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject leftArrow;
    [SerializeField]
    private GameObject rightArrow;
    [SerializeField]
    private GameObject confirm;

    //all the maps playable in the game
    private GameObject[] maps;
    private int currentMap;
    private string mapselected;
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        maps = GameObject.FindGameObjectsWithTag("mapIcon");

        maps[0].SetActive(true);
        for (int i = 1; i < maps.Length; ++i)
        {
            maps[i].SetActive(false);
        }
        currentMap = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSubmit()
    {
        if (confirm.activeSelf)
        {
            Debug.Log("Submit");
            Debug.Log("character 0 : " + maps[0].name + " character 1 : " + maps[1].name);
            Debug.Log("Current Character : " + currentMap);
            Debug.Log("character selected : " + maps[currentMap].name);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            confirm.SetActive(false);

            mapselected = maps[currentMap].name;
            selected = true;
        }
    }

    void OnCancel()
    {
        if (!confirm.activeSelf)
        {
            Debug.Log("Cancel");
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            confirm.SetActive(true);
            selected = false;
        }
        else
        {
            SceneManagerWithParameters.Load("menuPrincipal");
        }
    }

    void OnStart()
    {
        if (selected)
        {
            SceneManagerWithParameters.SetMap(mapselected);
            SceneManagerWithParameters.SetGamesToWin(5);
            
            SceneManagerWithParameters.Load("menuPerso");
        }
    }

    void OnLeft()
    {
        if (confirm.activeSelf && !(currentMap <= 0))
        {
            Debug.Log("Left");
            maps[currentMap - 1].SetActive(true);
            maps[currentMap].SetActive(false);
            currentMap--;
        }
        else
        {
            currentMap = 0;
        }
    }

    void OnRight()
    {
        if (confirm.activeSelf && !(currentMap >= maps.Length - 1))
        {
            Debug.Log("Right");
            maps[currentMap + 1].SetActive(true);
            maps[currentMap].SetActive(false);
            currentMap++;
        }
        else
        {
            currentMap = maps.Length - 1;
        }
    }
}
