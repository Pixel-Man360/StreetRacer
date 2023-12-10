using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public AudioClip clip;

    public float volume = 0.75f;
    public float pitch = 1;
    public bool loop;

    public AudioType audioType;
    [HideInInspector] public AudioSource source;
}

public enum AudioType
{
    Music,
    Sfx
}
