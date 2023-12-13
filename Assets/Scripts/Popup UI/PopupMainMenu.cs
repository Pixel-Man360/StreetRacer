using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMainMenu : PopupBase
{
    [SerializeField] private Transform carsHolder;
    private string carSaveHash = "Current Car";
    private int currentXPos = 0;
    private int currentCar = 0;
    public override void onDestroyView()
    {

    }

    public override void OnHideView()
    {

    }

    public override void OnShowView()
    {
        currentCar = PlayerPrefs.GetInt(carSaveHash, 0);


        carsHolder.transform.position = new(currentCar * -20f, 0, 0);
    }

    public override void OnStart()
    {
        currentCar = PlayerPrefs.GetInt(carSaveHash, 0);


        carsHolder.transform.position = new(currentCar * -20f, 0, 0);
    }
}
