using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RewardsManager : MonoBehaviour
{
    [SerializeField] private Canvas rewardsCanvas;
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private TMP_Text rewardTxt;
    [SerializeField] private RectTransform cashIcon;
    public static RewardsManager instance;

    void Awake()
    {
        instance = this;
    }

    public void GiveReward(int amount, Vector3 position)
    {
        rewardTxt.rectTransform.anchoredPosition3D = position;
        rewardTxt.SetText("+" + amount);
        rewardTxt.gameObject.SetActive(true);

        rewardTxt.rectTransform.DOAnchorPos3D(new(0, position.y + 350f, 0f), 1f).OnComplete
        (
            () =>
            {
                rewardTxt.gameObject.SetActive(false);
            }
        );

        List<RectTransform> temp = new();

        int required = amount / 20;
        int left = (amount % 20 == 0) ? 0 : amount % 20;

        for (int i = 0; i < required; i++)
        {
            GameObject cashImg = ObjectPool.instance.GetObject(moneyPrefab);

            Vector3 newPos = new(Random.Range(position.x - 80f, position.x + 80f), Random.Range(position.y - 80f, position.y + 80f), position.z);

            RectTransform cashTransform = cashImg.GetComponent<RectTransform>();
            temp.Add(cashTransform);

            cashTransform.localScale = Vector3.one;
            cashTransform.rotation = Quaternion.Euler(0, 0, 0);

            cashImg.SetActive(true);

            cashTransform.SetParent(rewardsCanvas.transform);

            cashTransform.anchoredPosition3D = position + new Vector3(0, 0, 0);
            cashTransform.localScale = Vector3.one;

            // Quaternion rot = 
            cashTransform.localRotation = Quaternion.identity;

            cashTransform.DOAnchorPos3D(newPos, 0.15f).SetEase(Ease.InBack);
        }

        if (left != 0)
        {
            GameObject cashImg = ObjectPool.instance.GetObject(moneyPrefab);

            Vector3 newPos = new(Random.Range(position.x - 80f, position.x + 80f), Random.Range(position.y - 80f, position.y + 80f), position.z);

            RectTransform cashTransform = cashImg.GetComponent<RectTransform>();
            temp.Add(cashTransform);

            cashTransform.localScale = Vector3.one;
            cashTransform.rotation = Quaternion.Euler(0, 0, 0);

            cashImg.SetActive(true);

            cashTransform.SetParent(rewardsCanvas.transform);

            cashTransform.anchoredPosition3D = position + new Vector3(0, 0, 0);
            cashTransform.localScale = Vector3.one;

            // Quaternion rot = 
            cashTransform.localRotation = Quaternion.identity;

            cashTransform.DOAnchorPos3D(newPos, 0.15f).SetEase(Ease.InBack);
        }

        StartCoroutine(ShowCashFlying(temp, required, left));
    }

    private IEnumerator ShowCashFlying(List<RectTransform> temp, int required, int left)
    {
        WaitForSeconds delay = new WaitForSeconds(0.075f);

        yield return new WaitForSeconds(0.25f);


        int Count = 0;

        foreach (RectTransform obj in temp)
        {
            obj.DOMove(cashIcon.position, 0.15f).OnComplete
            (
                () =>
                {
                    ObjectPool.instance.ReturnToPool(obj.gameObject);

                    LevelManager.instance.AddCashAmount((Count == required) ? left : 20);

                    SoundManager.instance.PlaySound(5);

                    Count++;
                }
            );

            yield return delay;
        }

        LevelManager.instance.OnRewardsGiven();
    }
}
