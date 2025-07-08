using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    public static int nextScene = 0;

    public static void ChangeScene(int scene)
    {
        nextScene = scene;

        SceneManager.LoadSceneAsync(1);
    }

    public static int GetScene()
    {
        return nextScene;
    }
}
