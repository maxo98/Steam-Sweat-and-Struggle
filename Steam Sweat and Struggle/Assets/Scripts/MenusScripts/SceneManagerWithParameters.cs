using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.SceneManagement;

public static class SceneManagerWithParameters
{

    private static List<string> parameters;

    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static List<string> GetSceneParameters()
    {
        return parameters;
    }

    public static void SetParam(string paramValue)
    {
        if (parameters == null)
        {
            parameters = new List<string>();
        }
        if (parameters.Contains(paramValue))
        {
            parameters.Remove(paramValue);
        }
        parameters.Add(paramValue);
    }

}