using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private bool canStartTimer = false;

    private float remainingTime;



    void Update()
    {
        if (!canStartTimer) return;


        CountDown();
    }

    public void StopTimer()
    {
        canStartTimer = false;
        timerText.gameObject.SetActive(false);
    }

    public void SetTimer(float maxTime)
    {
        this.remainingTime = maxTime;

        timerText.gameObject.SetActive(true);

        int mins = Mathf.FloorToInt(remainingTime / 60);
        int secs = Mathf.FloorToInt(remainingTime % 60);

        timerText.SetText(string.Format("{0:00}:{1:00}", mins, secs));

        canStartTimer = true;


        SetTimerText();

    }

    private void CountDown()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }

        else
        {
            canStartTimer = false;
            OnTimerEnd();
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        int mins = Mathf.FloorToInt(remainingTime / 60);
        int secs = Mathf.FloorToInt(remainingTime % 60);

        timerText.SetText(string.Format("{0:00}:{1:00}", mins, secs));
    }

    private void OnTimerEnd()
    {
        LevelManager.instance.OnRaceLost();
    }
}
