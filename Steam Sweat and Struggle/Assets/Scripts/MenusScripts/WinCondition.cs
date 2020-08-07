using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public CharacterName[] Numbers { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Numbers = GetComponentsInChildren<CharacterName>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
