using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text goText;
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
}
