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
}
