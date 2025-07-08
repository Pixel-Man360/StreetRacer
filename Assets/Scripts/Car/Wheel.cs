using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private WheelCollider wheelCollider;
    [SerializeField] private ParticleSystem smokeParticle;
    [SerializeField] private TrailRenderer trailPrefab;

    private TrailRenderer trailRenderer;

    void Start()
    {
        trailRenderer = Instantiate(trailPrefab, transform.position - Vector3.up * wheelCollider.radius, Quaternion.identity, transform.parent);
    }


    void Update()
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;

    }

    internal void HandleAcceleration(float motorSpeed)
    {
        wheelCollider.motorTorque = motorSpeed;
    }

    internal void ApplySteerAngle(float angle)
    {
        wheelCollider.steerAngle = angle;
    }

    internal void ApplyBrake(float power)
    {
        wheelCollider.brakeTorque = power;
    }

    internal void ApplyMotorTorque(float torque)
    {
        wheelCollider.motorTorque = torque;
    }

    internal void SetSmoke()
    {
        wheelCollider.GetGroundHit(out WheelHit hit);

        float slipAllowance = 1f;

        if (Mathf.Abs(hit.sidewaysSlip) + Mathf.Abs(hit.forwardSlip) > slipAllowance)
        {
            smokeParticle.Play();
            trailRenderer.emitting = true;
        }

        else
        {
            smokeParticle.Stop();
            trailRenderer.emitting = false;
        }
    }

    internal bool IsGrounded()
    {
        return wheelCollider.isGrounded;
    }

    internal float GetRPM()
    {
        return wheelCollider.rpm;
    }

    internal float GetRadius()
    {
        return wheelCollider.radius;
    }



}
