using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupLevelLost : PopupBase
{
    public override void onDestroyView()
    {

    }

    public override void OnHideView()
    {
    }

    public override void OnShowView()
    {

    }

    public override void OnStart()
    {

    }

    public void Resume()
    {
        LevelManager.instance.OnGoingToFreeRoam();
        this.HideView();
        Loader.ChangeScene(2);
    }

    public void Restart()
    {
        this.HideView();
        Loader.ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        this.HideView();
        Loader.ChangeScene(0);
    }
}
