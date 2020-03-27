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

    private CharacterName[] characters;
    private int currentCharacter;

    //tableau contenant le nom du perso et l'id du controller
    private string[] characterSelected = new string[2];

    //position of the current GameObject
    private RectTransform rectTransform;

    private bool firstInput = true;
    public bool selected = false;
    public bool destroyed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetParent(int playerIndex, GameObject newParent, CharacterName[] characters) {

        PlayerInput playerInput = GetComponent<PlayerInput>();
        //set the parent of the selector as the menu
        gameObject.transform.SetParent(newParent.transform);

        rectTransform = GetComponent<RectTransform>();

        //get all the characters implemented
        this.characters = characters;

        Debug.Log("number of character available : " + characters.Length);
        //display the first character on the selection menu for each new player
        characters[0].gameObject.SetActive(true);
        for (int i = 1; i < characters.Length; ++i)
        {
            characters[i].gameObject.SetActive(false);
        }
        currentCharacter = 0;

        //place the selector according to the player number
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

        if (confirm.activeSelf)
        {
            Debug.Log("Submit");
            Debug.Log("character 0 : " + characters[0].name + " character 1 : " + characters[1].name);
            Debug.Log("Current Character : " + currentCharacter);
            Debug.Log("character selected : " + characters[currentCharacter].name);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            confirm.SetActive(false);

            characterSelected[0] = characters[currentCharacter].name;
            characterSelected[1] = GetComponent<PlayerInput>().devices[0].path;
            selected = true;
        }
    }

    void OnCancel()
    {

        if (firstInput)
        {
            firstInput = false;
            return;
        }

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
            SceneManagerWithParameters.Load("menuMap");
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
            if (currentPlayer.selected)
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

        if (confirm.activeSelf && !(currentCharacter <= 0))
        {
            Debug.Log("Left");
            characters[currentCharacter - 1].gameObject.SetActive(true);
            characters[currentCharacter].gameObject.SetActive(false);
            currentCharacter --;
        }
        else
        {
            currentCharacter = 0;
        }
    }

    void OnRight()
    {
        if (firstInput)
        {
            firstInput = false;
            return;
        }
        if (confirm.activeSelf && !(currentCharacter >= characters.Length - 1))
        {
            Debug.Log("Right");
            characters[currentCharacter + 1].gameObject.SetActive(true);
            characters[currentCharacter].gameObject.SetActive(false);
            currentCharacter ++;
        }
        else
        {
            currentCharacter = characters.Length - 1;
        }
    }
}
