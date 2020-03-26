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
    [SerializeField]
    private GameObject character1;
    [SerializeField]
    private GameObject character2;
    [SerializeField]
    private GameObject character3;
    private RectTransform rectTransform;
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetParent(int playerIndex, GameObject newParent) {
        gameObject.transform.SetParent(newParent.transform);

        rectTransform = GetComponent<RectTransform>();

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

        rectTransform.localScale = new Vector2(100, 100);


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
