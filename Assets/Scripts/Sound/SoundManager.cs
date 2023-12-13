using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    private Dictionary<int, Sound> dictionary = new Dictionary<int, Sound>();
    public static SoundManager instance;

    void Awake()
    {
        instance = this;

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = this.gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = this.sounds[i].clip;
            sounds[i].source.volume = this.sounds[i].volume;
            sounds[i].source.pitch = this.sounds[i].pitch;
            sounds[i].source.loop = this.sounds[i].loop;

            dictionary[i] = sounds[i];

            if(sounds[i].audioType == AudioType.Sfx)
            {
                sounds[i].source.mute = PlayerPrefs.GetInt("SFX", 0) == 1;
            }

            else 
            {
                sounds[i].source.mute = PlayerPrefs.GetInt("Music", 0) == 1;
            }
        }
    }

    public void PlaySound(int serial)
    {
        if (dictionary.ContainsKey(serial))
        {
            dictionary[serial].source.Play();
        }

        else
            return;
    }

    public void StopSound(int serial)
    {
        if (dictionary.ContainsKey(serial))
        {
            dictionary[serial].source.Stop();
        }

        else
            return;
    }

    public void StopAllSFX()
    {
        foreach (KeyValuePair<int, Sound> sound in dictionary)
        {
            if (sound.Value.audioType == AudioType.Sfx)
                sound.Value.source.Stop();
        }
    }

    public void StopAllMusic()
    {
        foreach (KeyValuePair<int, Sound> sound in dictionary)
        {
            if (sound.Value.audioType == AudioType.Music)
                sound.Value.source.Stop();
        }
    }

    public void PlaySoundWithPriority(int serial)
    {
        StartCoroutine(PrioritySoundCoroutine(serial));
    }

    private IEnumerator PrioritySoundCoroutine(int serial)
    {
        float delVal = 0f;

        if (dictionary.ContainsKey(serial))
        {
            dictionary[serial].source.Play();
            delVal = dictionary[serial].source.clip.length;
        }

        WaitForSeconds delay = new WaitForSeconds(delVal);

        float val = 0f;

        foreach (KeyValuePair<int, Sound> sound in dictionary)
        {
            if (sound.Value.audioType == AudioType.Music)
            {
                val = sound.Value.source.volume;
                sound.Value.source.volume = 0.2f;
            }
        }

        yield return delay;

        foreach (KeyValuePair<int, Sound> sound in dictionary)
        {
            if (sound.Value.audioType == AudioType.Music)
            {
                sound.Value.source.volume = val;
            }
        }
    }

    public void MakeSoundOnOrOff(AudioType audioType, bool muteVal, Toggle settings)
    {
        foreach (KeyValuePair<int, Sound> audio in dictionary)
        {
            if (audio.Value.audioType == audioType)
            {
                audio.Value.source.mute = muteVal;
            }

            int saveVal = (muteVal == true) ? 1 : 0;
            PlayerPrefs.SetInt(audioType.ToString(), saveVal);

            int isOnVal = (saveVal == 1) ? 0 : 1;

            PlayerPrefs.SetInt("Settings " + audioType.ToString(), isOnVal);
        }
    }
}

