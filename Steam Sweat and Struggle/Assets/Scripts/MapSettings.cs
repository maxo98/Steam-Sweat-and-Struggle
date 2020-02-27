using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSettings : MonoBehaviour
{
    public float Top { get; set; }
    public float Bottom { get; set; }
    public float Left { get; set; }
    public float Right { get; set; }

    private GameObject[] objects;


    // Start is called before the first frame update
    void Start()
    {
        MapSizeChecker();
        InstantiatePlayers();

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

    void InstantiatePlayer()
    {

    }
}
