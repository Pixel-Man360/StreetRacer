using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SettingsPopup : PopupBase
{
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle hapticToggle;
    [SerializeField] private RectTransform[] handle;
    private string sfxHash = "SFX";
    private string musicHash = "Music";

    private bool isSfxOn;
    private bool isMusicOn;

    public static event Action<bool> OnSfxToggled;

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
        isSfxOn = (PlayerPrefs.GetInt(sfxHash, 1) == 1) ? true : false;
        isMusicOn = (PlayerPrefs.GetInt(musicHash, 1) == 1) ? true : false;

        sfxToggle.isOn = isSfxOn;
        musicToggle.isOn = isMusicOn;

        if (sfxToggle.isOn)
        {
            handle[0].localPosition = new Vector2(75, 0);
        }

        else if (!sfxToggle.isOn)
        {
            handle[0].localPosition = new Vector2(-75, 0);
        }

        if (musicToggle.isOn)
        {
            handle[1].localPosition = new Vector2(75, 0);
        }

        else if (!musicToggle.isOn)
        {
            handle[1].localPosition = new Vector2(-75, 0);
        }

        if (hapticToggle.isOn)
        {
            handle[2].localPosition = new Vector2(75, 0);
        }

        if (!hapticToggle.isOn)
        {
            handle[2].localPosition = new Vector2(-75, 0);
        }
    }

    public void OnSfxToggleClicked()
    {
        SoundManager.instance.MakeSoundOnOrOff(AudioType.Sfx, !sfxToggle.isOn, sfxToggle);

        isSfxOn = !sfxToggle.isOn;

        PlayerPrefs.SetInt(sfxHash, isSfxOn ? 1 : 0);

        if (sfxToggle.isOn)
        {
            handle[0].DOLocalMove(new Vector2(75, 0), 0.3f).SetEase(Ease.InOutBack).SetUpdate(true);
        }

        else if (!sfxToggle.isOn)
        {
            handle[0].DOLocalMove(new Vector2(-75, 0), 0.3f).SetEase(Ease.InOutBack).SetUpdate(true);
        }

        OnSfxToggled?.Invoke(sfxToggle.isOn);
    }

    public void OnMusicToggleClicked()
    {
        SoundManager.instance.MakeSoundOnOrOff(AudioType.Music, !musicToggle.isOn, musicToggle);
        isMusicOn = !musicToggle.isOn;

        PlayerPrefs.SetInt(musicHash, isMusicOn ? 1 : 0);

        if (musicToggle.isOn)
        {
            handle[1].DOLocalMove(new Vector2(75, 0), 0.3f).SetEase(Ease.InOutBack).SetUpdate(true);
        }

        else if (!musicToggle.isOn)
        {
            handle[1].DOLocalMove(new Vector2(-75, 0), 0.3f).SetEase(Ease.InOutBack).SetUpdate(true);
        }
    }

    public void OnHapticToggleClicked()
    {
        if (hapticToggle.isOn)
        {
            handle[2].DOLocalMove(new Vector2(75, 0), 0.3f).SetEase(Ease.InOutBack).SetUpdate(true);
        }

        else if (!hapticToggle.isOn)
        {
            handle[2].DOLocalMove(new Vector2(-75, 0), 0.3f).SetEase(Ease.InOutBack).SetUpdate(true);
        }
        
    }
    public void OnExitButtonClicked()
    {
        SoundManager.instance.PlaySound(4);
        this.HideView();
    }
}
