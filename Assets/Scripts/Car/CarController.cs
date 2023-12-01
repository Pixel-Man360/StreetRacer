using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private List<Wheel> wheels;

    [SerializeField] private float motorPower;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private AnimationCurve steerCurve;
    private float speed;



    void FixedUpdate()
    {
        Vector3 pos = new(input.horizontal, 0f, input.vertical);

        speed = rb.velocity.magnitude;

        if (pos == Vector3.zero) return;

        HandleAcceleration();
        HandleSteering();
    }

    private void HandleAcceleration()
    {
        foreach (Wheel wheel in wheels)
        {
            wheel.HandleAcceleration(motorPower * input.vertical);
        }
    }

    private void HandleSteering()
    {
        float streeringAngle = input.horizontal * steerCurve.Evaluate(speed);
        wheels[0].ApplySteerAngle(streeringAngle);
        wheels[1].ApplySteerAngle(streeringAngle);
    }





}

