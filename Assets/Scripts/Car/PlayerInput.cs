using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private MunStudios.CarController carController;
    internal float steerInput = 0;
    internal float gasInput = 0;

    void Update()
    {
        steerInput = Input.GetAxis("Horizontal");
        gasInput = Input.GetAxis("Vertical");

        carController.SetInput(gasInput, steerInput, 0, 0);
    }
}
