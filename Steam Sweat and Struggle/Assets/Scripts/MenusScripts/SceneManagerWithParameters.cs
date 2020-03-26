using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagerWithParameters
{
    public struct Parameters
    {
        public string MapName { get; set; }
        public string[][] CharactersSelected { get; set; }
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
        if (parameters.MapName == null && parameters.CharactersSelected == null)
        {
            parameters = new Parameters();
        }
        parameters.MapName = mapName;
    }

    public static void SetCharacters(string[][] charactersSelected)
    {
        parameters.CharactersSelected = charactersSelected;
    }

}