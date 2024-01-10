using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] CheckPoint checkpointPrefab;
    [SerializeField] CheckPoint startLinePrefab;
    [SerializeField] CheckPoint endLinePrefab;
    [SerializeField] GameObject directionPrefab;
    [SerializeField] private List<RaceData> allRaceData;
    [SerializeField] private List<AiCarController> aiRaceCars;

    private string currentRaceHash = "Current Race Id";
    private int currentRaceId;
    private RaceData currentRaceData;

    private CheckPoint startLine;
    private CheckPoint endLine;
    private List<CheckPoint> checkPoints = new();
    private CheckPoint currentCheckPoint;
    private List<AiCarController> raceCarsSpawned = new();
    private int ckId;
    private int aiEnteredPoints = 0;
    public static RaceManager instance;



    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentRaceId = PlayerPrefs.GetInt(currentRaceHash, 0);
        currentRaceData = GetRaceDataById(currentRaceId);

        LoadLevel();
    }

    private void LoadLevel()
    {
        CameraFollow.instance.SetFollowState(false);

        int currentCheckpointId = 0;

        Debug.LogError(currentRaceData);

        startLine = Instantiate(startLinePrefab, currentRaceData.startingLine.position, currentRaceData.startingLine.rotation);
        startLine.SetCheckPointId(currentCheckpointId);

        foreach (PointSavedData data in currentRaceData.checkPoints)
        {
            currentCheckpointId++;
            CheckPoint checkPoint = Instantiate(checkpointPrefab, data.position, data.rotation);
            checkPoint.SetCheckPointId(currentCheckpointId);
            checkPoints.Add(checkPoint);
        }

        currentCheckpointId++;

        endLine = Instantiate(endLinePrefab, currentRaceData.finishLine.position, currentRaceData.finishLine.rotation);
        endLine.SetCheckPointId(currentCheckpointId);

        currentCheckPoint = startLine;

        foreach (PointSavedData data in currentRaceData.directionPoints)
        {
            Instantiate(directionPrefab, data.position, data.rotation);
        }

        LevelManager.instance.SpawnPlayer(currentRaceData.playerStartPoint.position, currentRaceData.playerStartPoint.rotation);

        foreach (PointSavedData data in currentRaceData.otherRacersStartPoint)
        {
            AiCarController aiCarController = Instantiate(aiRaceCars[UnityEngine.Random.Range(0, aiRaceCars.Count)], data.position, data.rotation);
            raceCarsSpawned.Add(aiCarController);
            aiCarController.SetDrivingState(AIState.Idle);
            aiCarController.SetCurrentCheckPoint(startLine);
        }

        CameraFollow.instance.PanToPlayer(StartLevel);
    }

    public void StartLevel()
    {
        LevelManager.instance.SetPlayerDriveState(true);
        UIManager.instance.ShowGoText();
        LevelManager.instance.OnRaceStart(currentRaceData);
        SoundManager.instance.PlaySound(1);
    }

    public void OnSprintStarted()
    {
        foreach (AiCarController aiCarController in raceCarsSpawned)
        {
            aiCarController.SetDrivingState(AIState.Driving);
        }
    }

    private RaceData GetRaceDataById(int id)
    {
        foreach (RaceData data in allRaceData)
        {
            if (currentRaceId == data.raceId)
            {
                return data;
            }
        }

        return null;
    }

    public void UpdateCheckPoint(CheckPoint enteredPoint)
    {
        if (currentCheckPoint.GetCheckPointId() != enteredPoint.GetCheckPointId()) return;

        if (currentCheckPoint.GetCheckPointId() == startLine.GetCheckPointId())
        {
            currentCheckPoint = checkPoints[0];
        }

        else if (currentCheckPoint.GetCheckPointId() == endLine.GetCheckPointId())
        {
            //Level Won
            LevelManager.instance.OnRaceWon(currentRaceData.raceType);
        }

        else
        {
            currentCheckPoint = GetCheckPointById(enteredPoint.GetCheckPointId() + 1);
        }

        ckId = currentCheckPoint.GetCheckPointId();
    }

    public void OnAIEnteredCheckPoint(CheckPoint enteredPoint, AiCarController aiCarController)
    {
        if (aiCarController.GetCurrentCheckPoint().GetCheckPointId() != enteredPoint.GetCheckPointId()) return;
        if (aiCarController.GetCurrentCheckPoint().GetCheckPointId() == startLine.GetCheckPointId())
        {
            aiCarController.SetCurrentCheckPoint(checkPoints[0]);
        }

        else if (aiCarController.GetCurrentCheckPoint().GetCheckPointId() == endLine.GetCheckPointId())
        {
            aiCarController.OnRaceWon();
            aiCarController.SetDrivingState(AIState.Idle);
            aiEnteredPoints++;
        }

        else
        {
            aiCarController.SetCurrentCheckPoint(GetCheckPointById(enteredPoint.GetCheckPointId() + 1));
        }
    }

    private CheckPoint GetCheckPointById(int id)
    {
        if (id != checkPoints.Count)
        {
            foreach (CheckPoint checkPoint in checkPoints)
            {
                if (checkPoint.GetCheckPointId() == id)
                {
                    return checkPoint;
                }
            }
        }

        return endLine;
    }

    public RaceData GetRaceData()
    {
        return currentRaceData;
    }

    public int GetPlayerPoisition()
    {
        return aiEnteredPoints + 1;
    }
}
