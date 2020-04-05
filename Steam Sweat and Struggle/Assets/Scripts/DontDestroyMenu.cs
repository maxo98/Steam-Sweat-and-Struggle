using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class DontDestroyMenu : MonoBehaviour
{

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        Debug.Log(objs.Length);
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Map1")
        {
            Destroy(this.gameObject);
        }
    }
}