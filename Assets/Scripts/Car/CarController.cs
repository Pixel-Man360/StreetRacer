using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    // [SerializeField] private Rigidbody carRigidBody;
    // [SerializeField] private List<Wheel> wheels;


    // [Space]
    // [Header("Suspension:")]
    // [SerializeField] private float sprintStrength = 100f;
    // [SerializeField] private float springDamping = 15f;

    // [Space]
    // [Header("Steering")]
    // [SerializeField][Range(0, 1)] private float tireGrip = 0.5f;
    // [SerializeField] private float tireMass = 10f;

    // [Space]
    // [Header("Acceleration and Brake:")]
    // [SerializeField] private float topSpeed = 150f;
    // [SerializeField] private AnimationCurve powerCurve;


    // void FixedUpdate()
    // {
    //     //HandleMovement();
    // }



    private void HandleMovement()
    {
        // for (int i = 0; i < wheels.Count; i++)
        // {
        //     RaycastHit raycastHit;

        //     bool hit = Physics.Raycast(wheels[i].transform.position, -wheels[i].transform.up, out raycastHit, 5f, 1 << 6);

        //     if (hit)
        //     {
        //         UpdateSuspension(wheels[i], raycastHit);
        //         UpdateAcceleration(wheels[i]);
        //         UpdateSteering(wheels[i]);

        //     }

        // }
    }




    // private void UpdateSuspension(Wheel wheel, RaycastHit raycastHit)
    // {

    //     Debug.DrawRay(wheel.transform.position, -wheel.transform.up * 5f, Color.red);

    //     Vector3 springDir = wheel.transform.up;

    //     Vector3 wheelVelocity = carRigidBody.GetPointVelocity(wheel.transform.position);

    //     float offset = wheel.GetWheelRestDist() - raycastHit.distance;

    //     float velocity = Vector3.Dot(springDir, wheelVelocity);

    //     float force = (offset * sprintStrength) - (velocity * springDamping);


    //     carRigidBody.AddForceAtPosition(springDir * force, wheel.transform.position);

    //     Vector3 pos = wheel.transform.position;

    //     wheel.transform.position = new Vector3(pos.x, pos.y - offset, pos.z);
    // }

    // private void UpdateSteering(Wheel wheel)
    // {
    //     Vector3 steerDir = wheel.transform.right;
    //     Vector3 wheelVelocity = carRigidBody.GetPointVelocity(wheel.transform.position);

    //     float steeringVelocity = Vector3.Dot(steerDir, wheelVelocity);

    //     float desiredChange = -steeringVelocity * tireGrip;

    //     float desiredAcceleration = desiredChange / Time.fixedDeltaTime;

    //     carRigidBody.AddForceAtPosition(steerDir * tireMass * desiredAcceleration, wheel.transform.position);

    // }

    // private void UpdateAcceleration(Wheel wheel)
    // {
    //     Vector3 dir = wheel.transform.forward;

    //     if (input.vertical > 0f)
    //     {
    //         float carSpeed = Vector3.Dot(transform.forward, carRigidBody.velocity);

    //         float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed / topSpeed));

    //         float availableTorque = powerCurve.Evaluate(normalizedSpeed) * input.vertical;

    //         Debug.Log(dir * availableTorque);

    //         carRigidBody.AddForceAtPosition(dir * availableTorque, wheel.transform.position);
    //     }
    // }

}

