using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    public void SetTexts(int timeToFinish, string position, string reward)
    {

        int mins = Mathf.FloorToInt(timeToFinish / 60);
        int secs = Mathf.FloorToInt(timeToFinish % 60);

        timerTxt.SetText(string.Format("Time Taken: {0:00}:{1:00}", mins, secs));
        positionTxt.SetText("Position: " + position);
        rewardTxt.SetText("Reward: " + reward);
    }
}
