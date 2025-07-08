using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TophudMoneyCount : MonoBehaviour
{
    [SerializeField]
    private TMP_Text moneyTxt;

    int cashAmount = 0;

    void Start()
    {
        moneyTxt.text = Utility.FormatNumber(LevelManager.instance.GetCurrentCash());

        cashAmount = LevelManager.instance.GetCurrentCash();
    }

    public void UpdateCashAmount(int amount)
    {
        cashAmount += amount;

        moneyTxt.text = Utility.FormatNumber(cashAmount);
    }

    public void CashTextCollectionPopEffect()
    {
        moneyTxt.transform.DOScale(0.75f, 0.2f).OnComplete(() =>
        {
            moneyTxt.transform.DOScale(1f, 0.2f);
        });
    }
}
