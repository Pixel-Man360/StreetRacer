using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allCars;
    [SerializeField] private Transform initPos;
    [SerializeField] private bool isFreeRoam = true;
    public static LevelManager instance;

    private string carSaveHash = "Current Car";
    private int currentCar = 0;
    private CarController currentCarController;
    private float raceStartTime;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentCar = PlayerPrefs.GetInt(carSaveHash, 0);

        if (isFreeRoam)
        {
            SpawnPlayer(initPos.position, Quaternion.identity);
            currentCarController.SetDriveState(true);
        }
    }

    public void SetPlayerDriveState(bool val)
    {
        currentCarController.SetDriveState(val);
    }

    public void SpawnPlayer(Vector3 position, Quaternion rotation)
    {
        GameObject obj = Instantiate(allCars[currentCar], position, rotation);
        currentCarController = obj.GetComponent<CarController>();
        CameraFollow.instance.SetTarget(currentCarController.gameObject, currentCarController.GetCameraOffset().gameObject);
    }

    public void OnRaceStart(RaceData raceData)
    {
        raceStartTime = Time.time;

        switch(raceData.raceType)
        {
            case RaceType.TimeAttack:
            UIManager.instance.SetTimer(raceData.raceTime);
            break;
        }
    }

    public void OnRaceWon(RaceType raceType)
    {
        switch (raceType)
        {
            case RaceType.TimeAttack:
                currentCarController.SetDriveState(false);
                Debug.Log("Race Won! Show Score UI And Give Rewards");
                Debug.Log("Finish Time: " + (Time.time - raceStartTime));
                break;
        }
    }

    public void OnRaceLost()
    {

    }
}

