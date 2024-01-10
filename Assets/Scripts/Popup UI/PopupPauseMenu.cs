using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupPauseMenu : PopupBase
{
    public override void onDestroyView()
    {
        Time.timeScale = 1;
    }

    public override void OnHideView()
    {
        Time.timeScale = 1;
    }

    public override void OnShowView()
    {
        Time.timeScale = 0;
    }

    public override void OnStart()
    {
        
    }

    public void Resume()
    {
        this.HideView();
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
