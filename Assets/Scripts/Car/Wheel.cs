using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private WheelCollider wheelCollider;


    void Update()
    {

        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;

    }

    public void HandleAcceleration(float motorSpeed)
    {
        wheelCollider.motorTorque = motorSpeed;
    }

    public void ApplySteerAngle(float angle)
    {
        wheelCollider.steerAngle = angle;
    }

}
