using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public static class SceneManagerWithParameters
{
    public struct Parameters
    {
        public string MapName { get; set; }
        public Dictionary<string, InputDevice> CharactersSelected { get; set; }
        public Dictionary<string, int> Scores { get; set; }
        public int GamesToWin { get; set; }
    }

    private static Parameters parameters;

    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static Parameters GetSceneParameters()
    {
        return parameters;
    }

    public static void SetMap(string mapName)
    {
        if (parameters.MapName == null && parameters.CharactersSelected == null && parameters.GamesToWin == null)
        {
            parameters = new Parameters();
        }
        parameters.MapName = mapName;
    }

    public static void SetCharacters(Dictionary<string, InputDevice> charactersSelected)
    {
        parameters.CharactersSelected = charactersSelected;
        parameters.Scores = new Dictionary<string, int>();
        foreach(string s in charactersSelected.Keys)
        {
            parameters.Scores.Add(s, 0);
        }
    }

    public static void SetGamesToWin(int gameToWin)
    {
        if (parameters.MapName == null && parameters.CharactersSelected == null && parameters.GamesToWin == null)
        {
            parameters = new Parameters();
        }
        parameters.GamesToWin = gameToWin;
    }

}