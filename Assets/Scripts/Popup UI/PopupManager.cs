using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public PopupLevelLost popupLevelLost;
    public PopupRaceEnd popupRaceEnd;
    public PopupPauseMenu popupPauseMenu;
    public SettingsPopup settingsPopup;

    private static PopupManager instance;
    private Stack<IDeviceBackButtonInterface> allPopups;

    private bool isPaused = false;


    private void Awake()
    {
        instance = this;
        allPopups = new Stack<IDeviceBackButtonInterface>();
    }


    public static PopupManager GetInstance()
    {
        return instance;
    }

    public void Push(IDeviceBackButtonInterface item)
    {
        allPopups.Push(item);
    }

    public void Pop()
    {
        if (allPopups.Count > 0)
        {
            IDeviceBackButtonInterface top = allPopups.Peek();
            if (top.IsPopableOnDeviceBackBtn())
            {
                IDeviceBackButtonInterface item = allPopups.Pop();
                if (item != null)
                {
                    item.OnDeviceBackButtonPressed();
                }
            }
        }
        // else
        // {
        //     ShowAppExitConfirmationPopup();
        // }
    }

    public void Pop(IDeviceBackButtonInterface item)
    {
        if (allPopups.Count > 0 && allPopups.Contains(item))
        {
            List<IDeviceBackButtonInterface> idbi = new List<IDeviceBackButtonInterface>(allPopups.ToArray());
            this.allPopups.Clear();
            idbi.Remove(item);
            int length = idbi.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                this.allPopups.Push(idbi[i]);
            }
        }
    }

    private void PrintAllPopups()
    {
        foreach (var item in this.allPopups)
        {
            Debug.LogError(item.GetType().ToString());
        }
    }

    public void PopAll()
    {
        for (int i = 0; i < allPopups.Count; ++i)
        {
            IDeviceBackButtonInterface item = allPopups.Pop();
            if (item != null)
            {
                item.OnDeviceBackButtonPressed();
            }
        }
    }

    public int GetActivePopupCount()
    {
        return this.allPopups.Count;
    }

    public IDeviceBackButtonInterface GetTopActivePopup()
    {
        return allPopups.Peek();
    }

    // public void ShowAppExitConfirmationPopup()
    // {
    //     this.popupGeneric.ShowView();
    // }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                isPaused = true;

                popupPauseMenu.ShowView();
            }

            else  
            {
                isPaused = false;

                popupPauseMenu.HideView();
            }
        }
    }

    private void OnDestroy()
    {
        instance = null;
        allPopups.Clear();
    }
}