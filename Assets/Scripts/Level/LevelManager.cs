using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allCars;
    [SerializeField] private Transform initPos;
    [SerializeField] private CameraFollow cameraFollow;
    public static LevelManager instance;

    private string carSaveHash = "Current Car";
    private int currentCar = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentCar = PlayerPrefs.GetInt(carSaveHash, 0);

        GameObject obj = Instantiate(allCars[currentCar], initPos.position, Quaternion.identity);

        cameraFollow.SetTarget(obj, obj.GetComponent<CarController>().GetCameraOffset().gameObject);
    }
}
