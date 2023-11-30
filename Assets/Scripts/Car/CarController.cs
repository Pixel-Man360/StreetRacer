using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private List<Wheel> wheels;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxSteeringAngle;



    void FixedUpdate()
    {
        Vector3 pos = new(input.horizontal, 0f, input.vertical);

        if (pos == Vector3.zero) return;

        HandleAcceleration();


    }

    private void HandleAcceleration()
    {
        foreach (Wheel wheel in wheels)
        {
            wheel.HandleAcceleration(maxSpeed * Input.GetAxis("Vertical"));
        }
    }





}

