using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapSettings : MonoBehaviour
{
    public float Top { get; set; }
    public float Bottom { get; set; }
    public float Left { get; set; }
    public float Right { get; set; }

    private GameObject[] objects;
    private List<GameObject> player = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        MapSizeChecker();
    }

    private void OnEnable()
    {
        Dictionary<string, InputDevice> characters = SceneManagerWithParameters.GetSceneParameters().CharactersSelected;
        int i = 0;
        List<GameObject> list_spots_unrandomized = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spot"));
        List<GameObject> list_spots = new List<GameObject>();
        System.Random rand = new System.Random();
        //Randomize list
        while (list_spots_unrandomized.Count>0) {
            int x = rand.Next(list_spots_unrandomized.Count);
            list_spots.Add(list_spots_unrandomized[x]);
            list_spots_unrandomized.RemoveAt(x);
        }
        foreach(string s in characters.Keys)
        {
            PlayerInput playerInput = PlayerInput.Instantiate(prefab: (GameObject) Resources.Load("Prefab/characters/Character"+s), pairWithDevice: characters[s]);
            player.Add(playerInput.gameObject);
            playerInput.gameObject.GetComponent<Teleportation>().SetMapData(gameObject);
            playerInput.gameObject.transform.position = list_spots[i].transform.position;
            ++i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MapSizeChecker()
    {
        objects = GameObject.FindGameObjectsWithTag("Positions");
        Top = Bottom = objects[0].transform.position.y;
        Left = Right = objects[0].transform.position.x;
        foreach (GameObject g in objects)
        {
            if (Top < g.transform.position.y)
            {
                Top = g.transform.position.y;
            }
            else if (Bottom > g.transform.position.y)
            {
                Bottom = g.transform.position.y;
            }
            if (Left > g.transform.position.x)
            {
                Left = g.transform.position.x;
            }
            else if (Right < g.transform.position.x)
            {
                Right = g.transform.position.x;
            }
        }
    }

}
