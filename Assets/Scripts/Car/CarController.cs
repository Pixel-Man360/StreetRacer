using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float speed;
    private float speedClamped;
    private float slipAngle;
    private float brakeInput;

    [HideInInspector] public int isEngineRunning;

    void Start()
    {
        Physics.gravity = new Vector3(0, Physics.gravity.y * fallspeed, 0);
    }



    void FixedUpdate()
    {
        speed = wheels[2].GetRPM() * wheels[2].GetRadius() * 2f * Mathf.PI / 10f;
        speedClamped = Mathf.Lerp(speedClamped, speed, Time.deltaTime);

        CheckEngineAudio();
        //  FallingControl();

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

    private void FallingControl()
    {
        bool isGrounded = true;

        foreach (Wheel wheel in wheels)
        {
            if (!wheel.IsGrounded())
            {
                isGrounded = false;
                break;
            }
        }

        if (isGrounded) return;

        rb.velocity = Vector3.down * fallspeed * Time.fixedDeltaTime;
    }

    private void CheckSlip()
    {
        slipAngle = Vector3.Angle(transform.forward, rb.velocity - transform.forward);

        if (slipAngle < 120f)
        {
            if (input.gasInput < 0)
            {
                brakeInput = Mathf.Abs(input.gasInput);
                input.gasInput = 0;
            }
            else
            {
                brakeInput = 0;
            }
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
        //if (isEngineRunning > 1)
        // {
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
        // }
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
        float streeringAngle = input.steerInput * steerCurve.Evaluate(speed);
        //  streeringAngle += Vector3.SignedAngle(transform.forward, rb.velocity + transform.forward, Vector3.up);
        streeringAngle = Mathf.Clamp(streeringAngle, -60f, 60f);
        wheels[0].ApplySteerAngle(streeringAngle);
        wheels[1].ApplySteerAngle(streeringAngle);
    }


    internal float GetSpeedRatio()
    {
        float gas = Mathf.Clamp(Mathf.Abs(input.gasInput), 0.5f, 1f);
        return speedClamped * gas / maxSpeed;
    }





}

