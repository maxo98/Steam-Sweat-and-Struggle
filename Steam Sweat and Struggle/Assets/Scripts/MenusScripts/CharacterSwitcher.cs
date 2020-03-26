using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{

    private int idPlayer;

    [SerializeField]
    private GameObject leftArrow;
    [SerializeField]
    private GameObject rightArrow;
    [SerializeField]
    private GameObject confirm;

    //all the characters playable in the game
    private GameObject[] characters;
    private int currentcharacter;

    //tableau contenant le nom du perso et l'id du controller
    private string[] characterSelected = new string[2];

    //position of the current GameObject
    private RectTransform rectTransform;

    private bool firstInput = true;
    public bool Selected { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {

    }

    public void SetParent(int playerIndex, GameObject newParent) {
        gameObject.transform.SetParent(newParent.transform);

        rectTransform = GetComponent<RectTransform>();

        characters = GameObject.FindGameObjectsWithTag("characterIcon");
        characters[0].SetActive(true);
        for (int i = 1; i < characters.Length; ++i)
        {
            characters[i].SetActive(false);
        }
        currentcharacter = 0;

        idPlayer = playerIndex;

        switch (idPlayer)
        {

            case 1:
                Debug.Log("player number :" + idPlayer);
                rectTransform.localPosition = new Vector2(-600, 20);
                break;
            case 2:
                Debug.Log("player number :" + idPlayer);
                rectTransform.localPosition = new Vector2(-200, 20);
                break;
            case 3:
                Debug.Log("player number :" + idPlayer);
                rectTransform.localPosition = new Vector2(200, 20);
                break;
            case 4:
                Debug.Log("player number :" + idPlayer);
                rectTransform.localPosition = new Vector2(600, 20);
                break;
        }

        rectTransform.localScale = new Vector3(100, 100, 1);
        rectTransform.ForceUpdateRectTransforms();


    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnSubmit()
    {
        if (firstInput)
        {
            firstInput = false;
            return;
        }

        if (leftArrow.activeSelf)
        {
            Debug.Log("Submit");
            Debug.Log("character 0 : " + characters[0].name + " character 1 : " + characters[1].name);
            Debug.Log("Current Character : " + currentcharacter);
            Debug.Log("character selected : " + characters[currentcharacter].name);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            confirm.SetActive(false);

            characterSelected[0] = characters[currentcharacter].name;
            characterSelected[1] = gameObject.GetComponent<PlayerInput>().devices[0].deviceId.ToString();
            Selected = true;
        }
    }

    void OnCancel()
    {

        if (firstInput)
        {
            firstInput = false;
            return;
        }

        if (!leftArrow.activeSelf)
        {
            Debug.Log("Cancel");
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            confirm.SetActive(true);
            Selected = false;
        }
        else
        {
            SceneManagerWithParameters.Load("menuPrincipal");
        }
    }


    void OnStart()
    {
        if (firstInput)
        {
            firstInput = false;
            return;
        }
        
        GameObject[] characterSelecters = GameObject.FindGameObjectsWithTag("characterSelectionMenu");
        int numberSelected = 0;
        string[][] characters = new string[characterSelecters.Length][];
        foreach (GameObject g in characterSelecters)
        {
            CharacterSwitcher currentPlayer = g.GetComponent<CharacterSwitcher>();
            if (currentPlayer.Selected)
            {
                characters[numberSelected] = currentPlayer.characterSelected;
                numberSelected++;
            }
        }
        if (numberSelected == characterSelecters.Length)
        {
            SceneManagerWithParameters.SetCharacters(characters);

            SceneManagerWithParameters.Load(SceneManagerWithParameters.GetSceneParameters().MapName);
        }
        
    }

    void OnLeft()
    {
        if (firstInput)
        {
            firstInput = false;
            return;
        }

        if (leftArrow.activeSelf && currentcharacter == 0)
        {
            Debug.Log("Left");
            characters[currentcharacter].SetActive(false);
            characters[currentcharacter++].SetActive(true);
        }
    }

    void OnRight()
    {
        if (firstInput)
        {
            firstInput = false;
            return;
        }
        if (leftArrow.activeSelf && currentcharacter == characters.Length)
        {
            Debug.Log("Right");
            characters[currentcharacter].SetActive(false);
            characters[currentcharacter--].SetActive(true);
            
        }
    }
}
