using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    internal float steerInput = 0;
    internal float gasInput = 0;

    void Update()
    {
        steerInput = Input.GetAxis("Horizontal");
        gasInput = Input.GetAxis("Vertical");
    }
}
