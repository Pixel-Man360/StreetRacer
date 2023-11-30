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

        Debug.Log("Col Pos: " + position, wheelCollider.transform.gameObject);

        transform.position = position;
        transform.rotation = rotation;

        Debug.Log("Wheel Pos: " + transform.position, transform.gameObject);
    }

}
