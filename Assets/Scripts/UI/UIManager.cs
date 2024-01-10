using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text goText;
    [SerializeField] private TMP_Text rpmText;
    [SerializeField] private TMP_Text gearText;
    [SerializeField] private Transform rpmNeedle;
    [SerializeField] private TMP_Text levelEndText;
    [SerializeField] private TophudMoneyCount tophudMoneyCount;
    [SerializeField] private Timer timer;
    public static UIManager instance;

    void Awake()
    {
        instance = this;
    }

    public void ShowGoText()
    {
        goText.gameObject.SetActive(true);

        goText.rectTransform.DOAnchorPosY(200f, 1f).OnComplete
        (
            () =>
            {
                goText.rectTransform.anchoredPosition3D = Vector3.zero;
                goText.gameObject.SetActive(false);
            }
        );
    }

    public void ShowLevelEndText(string text)
    {
        levelEndText.gameObject.SetActive(true);
        levelEndText.SetText(text);
        levelEndText.rectTransform.DOAnchorPosY(200f, 1f).OnComplete
        (
            () =>
            {
                levelEndText.rectTransform.anchoredPosition3D = Vector3.zero;
                levelEndText.gameObject.SetActive(false);
            }
        );

    }

    public void SetNeedleRotation(Quaternion rot)
    {
        rpmNeedle.rotation = rot;
    }

    public void SetRpmText(string txt)
    {
        rpmText.text = txt;
    }

    public void SetGearText(string txt)
    {
        gearText.text = txt;
    }

    public void SetTimer(float maxTime)
    {
        timer.SetTimer(maxTime);
    }

    public void HideTimer()
    {
        timer.StopTimer();
    }

    public void UpdateTopHudMoneyCount(int amount)
    {
        tophudMoneyCount.UpdateCashAmount(amount);
    }
}
