using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupRaceSelector : PopupBase
{
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

    }

    public override void OnStart()
    {

    }

    public void OnRaceSelected()
    {
        Loader.ChangeScene(3);
    }
}