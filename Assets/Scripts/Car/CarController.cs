using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private EngineAudio engineAudio;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private List<Wheel> wheels;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float motorPower;
    [SerializeField] private float brakePower;
    [SerializeField] private AnimationCurve steerCurve;
    [SerializeField] private float fallspeed = 3f;
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private Transform cameraOffset;
    private float speed;
    private float speedClamped;
    private float slipAngle;
    private float brakeInput;
    public float RPM;
    public float redLine;
    public float idleRPM;
    public TMP_Text rpmText;
    public TMP_Text gearText;
    public Transform rpmNeedle;
    public float minNeedleRotation;
    public float maxNeedleRotation;
    public int currentGear;

    public float[] gearRatios;
    public float differentialRatio;
    private float currentTorque;
    private float clutch;
    private float wheelRPM;
    public AnimationCurve hpToRPMCurve;
    private GearState gearState;
    public float increaseGearRPM;
    public float decreaseGearRPM;
    public float changeGearTime = 0.5f;

    public GameObject tireTrail;
    public Material brakeMaterial;
    public Color brakingColor;
    public float brakeColorIntensity;

    [HideInInspector] public int isEngineRunning;

    void Start()
    {
        Physics.gravity = new Vector3(0, Physics.gravity.y * fallspeed, 0);
        rb.centerOfMass = centerOfMass.transform.localPosition;
    }

    // void Update()
    // {
    //     rpmNeedle.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(minNeedleRotation, maxNeedleRotation, RPM / (redLine * 1.1f)));
    //     rpmText.text = RPM.ToString("0,000") + "rpm";
    //     gearText.text = (gearState == GearState.Neutral) ? "N" : (currentGear + 1).ToString();
    // }



    void FixedUpdate()
    {
        speed = wheels[2].GetRPM() * wheels[2].GetRadius() * 2f * Mathf.PI / 10f;
        speedClamped = Mathf.Lerp(speedClamped, speed, Time.deltaTime);

        CheckEngineAudio();
        CheckSlip();
        CheckSmokeParticles();
        HandleAcceleration();
        HandleBraking();
        HandleSteering();
    }

    private void CheckEngineAudio()
    {
        if (Mathf.Abs(input.gasInput) > 0 && isEngineRunning == 0)
        {
            engineAudio.StartEngine();
        }

        else if (input.gasInput == 0)
        {
            if (Mathf.Abs(GetSpeedRatio()) <= 0.05f)
            {
                engineAudio.StopEngine();
            }
        }
    }

    private void CheckSlip()
    {
        slipAngle = Vector3.Angle(transform.forward, rb.velocity - transform.forward);
        float movingDirection = Vector3.Dot(transform.forward, rb.velocity);

        if (movingDirection < -0.5f && input.gasInput > 0)
        {
            brakeInput = Mathf.Abs(input.gasInput);
        }
        else if (movingDirection > 0.5f && input.gasInput < 0)
        {
            brakeInput = Mathf.Abs(input.gasInput);
        }
        else
        {
            brakeInput = 0;
        }
    }


    private void CheckSmokeParticles()
    {
        foreach (Wheel wheel in wheels)
        {
            wheel.SetSmoke();
        }
    }
    private void HandleAcceleration()
    {
        if (Mathf.Abs(speed) < maxSpeed)
        {
            wheels[0].HandleAcceleration(motorPower * input.gasInput);
            wheels[1].HandleAcceleration(motorPower * input.gasInput);
        }
        else
        {
            wheels[2].HandleAcceleration(0);
            wheels[3].HandleAcceleration(0);
        }
    }


    private void HandleBraking()
    {
        for (int i = 0; i < wheels.Count; i++)
        {
            if (i <= 1)
            {
                wheels[i].ApplyBrake(brakeInput * brakePower * 0.7f);
            }

            else
            {
                wheels[i].ApplyBrake(brakeInput * brakePower * 0.3f);
            }

        }
    }

    private void HandleSteering()
    {
        float steeringAngle = input.steerInput * steerCurve.Evaluate(speed);
        // if (slipAngle < 120f)
        // {
        //     steeringAngle += Vector3.SignedAngle(transform.forward, rb.velocity + transform.forward, Vector3.up);
        // }
        // steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);

        wheels[0].ApplySteerAngle(steeringAngle);
        wheels[1].ApplySteerAngle(steeringAngle);
    }


    internal float GetSpeedRatio()
    {
        float gas = Mathf.Clamp(Mathf.Abs(input.gasInput), 0.5f, 1f);
        return speedClamped * gas / maxSpeed;
    }

    public Transform GetCameraOffset()
    {
        return cameraOffset;
    }
}

public enum GearState
{
    Neutral,
    Running,
    CheckingChange,
    Changing
};


