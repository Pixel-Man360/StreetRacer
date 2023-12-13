using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectorItemUI : MonoBehaviour
{
    [SerializeField] private int carId;
    [SerializeField] private string carName;

    public int GetCarId() => carId;
    public string GetCarName() => carName;
}
