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

    private string currentRaceHash = "Current Race Id";
    private int currentRaceId;
    private RaceData currentRaceData;

    private CheckPoint startLine;
    private CheckPoint endLine;
    private List<CheckPoint> checkPoints = new();
    private CheckPoint currentCheckPoint;
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

        CameraFollow.instance.PanToPlayer(StartLevel);
    }

    public void StartLevel()
    {
        LevelManager.instance.SetPlayerDriveState(true);
        UIManager.instance.ShowGoText();
        LevelManager.instance.OnRaceStart();
        SoundManager.instance.PlaySound(1);
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

    private void UpdateCheckPoint(CheckPoint enteredPoint)
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
            GetCheckPointById(enteredPoint.GetCheckPointId() + 1);
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
}
