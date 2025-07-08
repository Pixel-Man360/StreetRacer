using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    void Update()
    {
        transform.Rotate(new(0, rotationSpeed * Time.deltaTime, 0));
    }
}
