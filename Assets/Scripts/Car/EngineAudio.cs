using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    [SerializeField] private CarController carController;
    [SerializeField] private AudioSource runningSound;
    [SerializeField] private float runningMaxVolume;
    [SerializeField] private float runningMaxPitch;
    [SerializeField] private AudioSource reverseSound;
    [SerializeField] private float reverseMaxVolume;
    [SerializeField] private float reverseMaxPitch;
    [SerializeField] private AudioSource idleSound;
    [SerializeField] private float idleMaxVolume;
    [SerializeField] private float LimiterSound = 1f;
    [SerializeField] private float LimiterFrequency = 3f;
    [SerializeField] private float LimiterEngage = 0.8f;
    private float revLimiter;
    private float speedRatio;
    private bool isEngineRunning = false;

    void Start()
    {
        idleSound.volume = idleMaxVolume;

        idleSound.Play();
        runningSound.volume = 0;
        reverseSound.volume = 0;
    }


    void Update()
    {
        float speedSign = 0;

        if (carController)
        {
            speedSign = Mathf.Sign(carController.GetSpeedRatio());
            speedRatio = Mathf.Abs(carController.GetSpeedRatio());
        }

        if (speedRatio > LimiterEngage)
        {
            revLimiter = (Mathf.Sin(Time.time * LimiterFrequency) + 1f) * LimiterSound * (speedRatio - LimiterEngage);
        }

        if (isEngineRunning)
        {
            idleSound.volume = Mathf.Lerp(idleMaxVolume, 0, speedRatio);

            idleSound.volume = 0;

            if (speedSign > 0)
            {
                //runningSound.Play();
                reverseSound.volume = 0;
                runningSound.volume = Mathf.Lerp(0.05f, runningMaxVolume, speedRatio);
                runningSound.pitch = Mathf.Lerp(runningSound.pitch, Mathf.Lerp(0.5f, runningMaxPitch, speedRatio) + revLimiter, Time.deltaTime);
            }
            else
            {
                runningSound.volume = 0;
                reverseSound.volume = Mathf.Lerp(0.05f, reverseMaxVolume, speedRatio);
                reverseSound.pitch = Mathf.Lerp(reverseSound.pitch, Mathf.Lerp(0.2f, reverseMaxPitch, speedRatio) + revLimiter, Time.deltaTime);
            }
        }
        else
        {
            //idleSound.Play();
            idleSound.volume = Mathf.Lerp(0, idleMaxVolume, speedRatio);
            idleSound.volume = Math.Clamp(idleSound.volume, 0.1f, idleMaxVolume);
            runningSound.volume = 0;
            reverseSound.volume = 0;
        }
    }
    public void StartEngine()
    {
        idleSound.Play();
        reverseSound.Play();
        runningSound.Play();

        isEngineRunning = true;
        carController.isEngineRunning = 2;
    }

    public void StopEngine()
    {
        carController.isEngineRunning = 0;
        isEngineRunning = false;
    }
}
