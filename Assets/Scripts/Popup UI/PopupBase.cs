using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class PopupBase : MonoBehaviour, IDeviceBackButtonInterface
{
    //  [SerializeField] private Image panelBackground;
    [SerializeField] protected bool closeOnOutsideTap;

    public static event Action OnPopupActive;
    public static event Action OnPopupInActive;


    void Start()
    {
        this.OnStart();
    }

    public void ShowView()
    {
        this.gameObject.SetActive(true);
        //panelBackground?.DOFade(1, 0.4f);
        this.PushToStack();
        this.OnShowView();
        OnPopupActive?.Invoke();
    }

    public void HideView()
    {
        // if (panelBackground != null)
        // {
        //     panelBackground.DOFade(0, 0.4f).OnComplete
        //     (
        //         () =>
        //         {
        //             this.gameObject.SetActive(false);
        //         }
        //     );

        // }

        // else
        // {
        this.gameObject.SetActive(false);
        //  }


        this.PopFromStack();
        this.OnHideView();

        OnPopupInActive?.Invoke();
    }

    public bool IsViewVisible()
    {
        return this.gameObject.activeSelf;
    }

    public void PopupCloseButtonPress()
    {
        this.HideView();
    }

    public void OnTappedOutsidePopup()
    {
        if (this.closeOnOutsideTap)
            this.HideView();
    }

    private void OnDestroy()
    {
        this.onDestroyView();
    }

    public void PushToStack()
    {
        if (PopupManager.GetInstance() != null)
        {
            PopupManager.GetInstance().Push(this);
        }
    }

    public void PopFromStack()
    {
        if (PopupManager.GetInstance() != null)
        {
            PopupManager.GetInstance().Pop(this);
        }
    }

    public virtual void OnDeviceBackButtonPressed()
    {
        // TODO: Add SFX
        //this.HideView();
    }

    public virtual bool IsPopableOnDeviceBackBtn()
    {
        return true;
    }

    public abstract void onDestroyView();
    public abstract void OnHideView();
    public abstract void OnShowView();
    public abstract void OnStart();

}
