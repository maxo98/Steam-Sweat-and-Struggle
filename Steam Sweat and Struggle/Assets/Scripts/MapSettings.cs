using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapSettings : MonoBehaviour
{
    public float Top { get; set; }
    public float Bottom { get; set; }
    public float Left { get; set; }
    public float Right { get; set; }

    private float shotSpeed = 1.0f;

    private GameObject[] objects;
    private Dictionary<string, GameObject> player = new Dictionary<string, GameObject>();

    public void SetShotSpeed(object v) {
        shotSpeed = (float) v;
    }
    public float GetShotSpeed() {
        return shotSpeed;
    }

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
            player.Add(s,playerInput.gameObject);
            playerInput.gameObject.GetComponent<Teleportation>().SetMapData(gameObject);
            playerInput.gameObject.transform.position = list_spots[i].transform.position;
            ++i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnDeath(object obj)
    {
        GameObject gameObject = (GameObject) obj;
        string name = "";
        foreach(string s in player.Keys)
        {
            if(player[s] == gameObject)
            {
                name = s;
            } 
        }
        player.Remove(name);
        Destroy(gameObject);
        if (player.Count == 1)
        {
            SceneManagerWithParameters.GetSceneParameters().Scores[player.Keys.First()] += 1;
            foreach(string s in SceneManagerWithParameters.GetSceneParameters().Scores.Keys)
            {
                Debug.Log(s + " Score : " + SceneManagerWithParameters.GetSceneParameters().Scores[s]);
            }
            SceneManagerWithParameters.Load("menuScore");
        }
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
