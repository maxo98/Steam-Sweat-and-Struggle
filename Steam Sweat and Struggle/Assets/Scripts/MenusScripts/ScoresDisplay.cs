using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresDisplay : MonoBehaviour
{
    private Dictionary<string, int> scores;
    private string[] scoreIcon = { "score1", "score2", "score3", "score4" };
    private bool winner = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("nombre de game a gagner : " + SceneManagerWithParameters.GetSceneParameters().GamesToWin);
    }

    private void OnEnable()
    {

        int i = 0;
        scores = SceneManagerWithParameters.GetSceneParameters().Scores;
        foreach(string s in scores.Keys)
        {
            if(scores[s] == SceneManagerWithParameters.GetSceneParameters().GamesToWin)
            {
                winner = true;
            }

            GameObject g = GameObject.FindGameObjectWithTag(scoreIcon[i++]);
            Sprite sprite = (Sprite)Resources.Load("Graphic/MenusGraphics/menuScore/icon" + s, typeof(Sprite));
            if(g == null)
            {
                Debug.LogError("Pas d'image loaded");
            }
            g.GetComponent<Image>().sprite = sprite;
            Image[] points = g.GetComponentsInChildren<Image>();
            int w = 0;
            int j = 0;
            int c = 0;
            foreach (Image img in points)
            {
                if (img.gameObject.transform.parent.gameObject == g)
                {

                    if (img.gameObject.tag == "emptyPoints" && w < SceneManagerWithParameters.GetSceneParameters().GamesToWin)
                    {
                        img.gameObject.SetActive(true);
                        w++;
                    }

                    else if (img.gameObject.tag == "wins" && j < scores[s])
                    {
                        img.gameObject.SetActive(true);
                        points[c - 1].gameObject.SetActive(false);
                        j++;
                    }

                    else if (img.gameObject.tag.Equals("emptyPoints") || img.gameObject.tag.Equals("wins"))
                    {
                        img.gameObject.SetActive(false);
                    }
                    
                    if(scores[s] < SceneManagerWithParameters.GetSceneParameters().GamesToWin && img.gameObject.tag.Equals("victory"))
                    {
                        img.gameObject.SetActive(false);
                    }
                }
                c++;
            }
        }
        for(; i < 4; ++i)
        {
            GameObject g = GameObject.FindGameObjectWithTag(scoreIcon[i]);
            g.SetActive(false);
        }
    }

    void OnSubmit()
    {
        if (!winner)
        {
            SceneManagerWithParameters.Load(SceneManagerWithParameters.GetSceneParameters().MapName);
        }
        else
        {
            SceneManagerWithParameters.Load("menuPrincipal");
        }
    }

    private void OnCancel()
    {
        if (!winner)
        {
            SceneManagerWithParameters.Load("MenuPrincipal");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
