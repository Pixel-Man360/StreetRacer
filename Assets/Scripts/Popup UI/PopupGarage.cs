using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupGarage : PopupBase
{
    [SerializeField] private Transform carsHolder;
    [SerializeField] private List<CarSelectorItemUI> carsToSelect;

    private int currentXPos = 0;
    private int currentCar = 0;

    private string carSaveHash = "Current Car";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentCar == carsToSelect.Count - 1) return;

            currentXPos -= 20;
            carsHolder.DOMoveX(currentXPos, 0.5f);
            currentCar++;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentCar == 0) return;

            currentXPos += 20;
            carsHolder.DOMoveX(currentXPos, 0.5f);
            currentCar--;
        }
    }

    public void OnCurrentCarSelected()
    {
        PlayerPrefs.SetInt(carSaveHash, currentCar);
    }

    public void OnBackButtonPressed()
    {

    }
    public override void onDestroyView()
    {

    }

    public override void OnHideView()
    {

    }

    public override void OnShowView()
    {
        currentCar = PlayerPrefs.GetInt(carSaveHash, 0);
        currentXPos = currentCar * -20;
        carsHolder.transform.position = new(currentXPos, 0, 0);
    }

    public override void OnStart()
    {
        currentCar = PlayerPrefs.GetInt(carSaveHash, 0);
        currentXPos = currentCar * -20;
        carsHolder.transform.position = new(currentXPos, 0, 0);
    }

    public void OnFreeRoamClicked()
    {
        Loader.ChangeScene(2);
    }
}