using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private List<Wheel> wheels;
    [SerializeField] private float motorPower;
    [SerializeField] private float brakePower;
    [SerializeField] private AnimationCurve steerCurve;
    private float speed;
    private float slipAngle;
    private float brakeInput;



    void FixedUpdate()
    {
        speed = rb.velocity.magnitude;

        CheckSlip();
        HandleAcceleration();
        HandleBraking();
        HandleSteering();
    }

    private void CheckSlip()
    {
        slipAngle = Vector3.SignedAngle(transform.forward, rb.velocity, Vector3.up);

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
    }

    private void HandleAcceleration()
    {
        foreach (Wheel wheel in wheels)
        {
            wheel.HandleAcceleration(motorPower * input.gasInput);
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
        float streeringAngle = input.steerInput * steerCurve.Evaluate(speed);
        wheels[0].ApplySteerAngle(streeringAngle);
        wheels[1].ApplySteerAngle(streeringAngle);
    }





}

