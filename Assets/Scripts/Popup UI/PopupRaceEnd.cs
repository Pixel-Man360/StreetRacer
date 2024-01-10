using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupRaceEnd : PopupBase
{
    [SerializeField] private TMP_Text timerTxt;
    [SerializeField] private TMP_Text positionTxt;
    [SerializeField] private TMP_Text rewardTxt;
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

    public void SetTexts(float timeToFinish, string position, string reward)
    {

        int mins = Mathf.FloorToInt(timeToFinish / 60);
        int secs = Mathf.FloorToInt(timeToFinish % 60);

        timerTxt.SetText(string.Format("Time Taken: {0:00}:{1:00}", mins, secs));
        positionTxt.SetText("Position: " + position);
        rewardTxt.SetText("Reward: " + reward);
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
        Loader.ChangeScene(1);
    }
}
