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
    private int currentReward;

    private RaceType wonRaceType;

    private EconomyData economyData;

    void Awake()
    {
        instance = this;
        economyData = SaveLoadManager.LoadEconomyData();

        if (economyData == null)
        {
            economyData = new EconomyData(0);
            SaveLoadManager.SaveEconomyData(economyData);
        }
    }

    void Start()
    {
        currentCar = PlayerPrefs.GetInt(carSaveHash, 0);

        if (isFreeRoam)
        {
            Vector3 pos = initPos.position;
            Quaternion rot = Quaternion.identity;
            SavedCarPos savedCarPos = SaveLoadManager.LoadCarPosData();

            if (savedCarPos != null)
            {
                pos = new(savedCarPos.x, savedCarPos.y, savedCarPos.z);
            }

            SpawnPlayer(pos, rot);
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

    public void OnGoingToFreeRoam()
    {
        Vector3 pos = currentCarController.transform.position;
        SavedCarPos savedCarPos = new(pos.x, pos.y, pos.z);

        SaveLoadManager.SavedCarPosData(savedCarPos);
    }

    public void OnRaceStart(RaceData raceData)
    {
        raceStartTime = Time.time;

        switch (raceData.raceType)
        {
            case RaceType.TimeAttack:
                UIManager.instance.SetTimer(raceData.raceTime);
                break;

            case RaceType.Sprint:
                RaceManager.instance.OnSprintStarted();
                break;
        }
    }

    public void OnRaceWon(RaceType raceType)
    {
        wonRaceType = raceType;
        switch (raceType)
        {
            case RaceType.TimeAttack:
                StartCoroutine(LevelWonSequence());
                break;

            case RaceType.Sprint:
                StartCoroutine(LevelWonSequence());
                break;
        }
    }

    private IEnumerator LevelWonSequence()
    {
        UIManager.instance.HideTimer();
        currentCarController.SetInput(-1, 90, 0, 0);
        currentCarController.SetDriveState(false);
        UIManager.instance.ShowLevelEndText("You Won");

        yield return new WaitForSeconds(1.25f);

        currentReward = RaceManager.instance.GetRaceData().rewardsPerPosition[0];

        RewardsManager.instance.GiveReward(currentReward, Vector3.zero);
    }

    public void OnRewardsGiven()
    {
        PopupManager.GetInstance().popupRaceEnd.SetTexts(Time.time - raceStartTime, "1", (wonRaceType == RaceType.TimeAttack) ? currentReward.ToString() : RaceManager.instance.GetPlayerPoisition().ToString());
        PopupManager.GetInstance().popupRaceEnd.ShowView();
    }

    public void OnRaceLost()
    {
        StartCoroutine(LevelLostSequence());
    }

    private IEnumerator LevelLostSequence()
    {
        UIManager.instance.HideTimer();
        currentCarController.SetInput(-1, 90, 0, 0);
        currentCarController.SetDriveState(false);
        UIManager.instance.ShowLevelEndText("You Lost");

        yield return new WaitForSeconds(1.25f);

        PopupManager.GetInstance().popupLevelLost.ShowView();
    }

    public void AddCashAmount(int amount)
    {
        economyData.coinCount += amount;

        UIManager.instance.UpdateTopHudMoneyCount(amount);

        SaveLoadManager.SaveEconomyData(economyData);
    }

    public int GetCurrentCash()
    {
        return economyData.coinCount;
    }
}

