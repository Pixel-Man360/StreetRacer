using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RaceCreator : MonoBehaviour
{
    public GameObject checkpointPrefab;
    public GameObject startLinePrefab;
    public GameObject endLinePrefab;
    public GameObject carPrefab;
    public GameObject directionPrefab;

    [Space]
    [Space]
    [Space]

    public AreaType areaType;
    public RaceType raceType;
    public GameObject startLine;
    public List<GameObject> checkPoints;
    public GameObject endLine;
    public GameObject playStartPoint;
    public List<GameObject> racerStartPoints;
    public List<GameObject> directionPoints;


    public void CreateStartingPoint()
    {
        startLine = Instantiate(startLinePrefab, this.transform);
    }

    public void CreateCheckPoint()
    {
        GameObject ck = Instantiate(checkpointPrefab, this.transform);

        if (checkPoints.Count > 0)
        {
            ck.transform.position = checkPoints[checkPoints.Count - 1].transform.position;
            ck.transform.rotation = checkPoints[checkPoints.Count - 1].transform.rotation;
        }

        checkPoints.Add(ck);
    }

    public void CreateEndingPoint()
    {
        endLine = Instantiate(endLinePrefab, this.transform);
    }

    public void CreatePlayerStartPoint()
    {
        playStartPoint = Instantiate(carPrefab, this.transform);
    }

    public void CreateRacerStartPoint()
    {
        GameObject ck = Instantiate(carPrefab, this.transform);

        if (racerStartPoints.Count > 0)
        {
            ck.transform.position = racerStartPoints[racerStartPoints.Count - 1].transform.position;
            ck.transform.rotation = racerStartPoints[racerStartPoints.Count - 1].transform.rotation;
        }

        racerStartPoints.Add(ck);
    }

    public void CreateDirectionPoint()
    {
        GameObject ck = Instantiate(directionPrefab, this.transform);

        if (directionPoints.Count > 0)
        {
            ck.transform.position = directionPoints[directionPoints.Count - 1].transform.position;
            ck.transform.rotation = directionPoints[directionPoints.Count - 1].transform.rotation;
        }

        directionPoints.Add(ck);
    }

    public void SaveLevel(string name = "")
    {
        RaceData asset = ScriptableObject.CreateInstance<RaceData>();
        FillRaceData(asset);

        string path;
        if (name != "")
            path = GetPath(name);
        else
            path = GetPath();
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(asset, path);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }

    private void FillRaceData(RaceData data)
    {
        data.checkPoints = new();

        foreach (GameObject obj in checkPoints)
        {
            data.checkPoints.Add(new(obj.transform.position, obj.transform.rotation));
        }

        data.finishLine = new(endLine.transform.position, endLine.transform.rotation);
        data.startingLine = new(startLine.transform.position, startLine.transform.rotation);

        data.playerStartPoint = new(playStartPoint.transform.position, playStartPoint.transform.rotation);

        data.otherRacersStartPoint = new();

        foreach (GameObject obj in racerStartPoints)
        {
            data.otherRacersStartPoint.Add(new(obj.transform.position, obj.transform.rotation));
        }

        data.directionPoints = new();

        foreach (GameObject obj in directionPoints)
        {
            data.directionPoints.Add(new(obj.transform.position, obj.transform.rotation));
        }

        data.raceType = raceType;
        data.areaType = areaType;
    }

    public void LoadLevel()
    {

    }

    private string GetPath(string name)
    {
        string path = "Assets/All Race Data/" + name + ".asset";
        return path;
    }

    private string GetPath()
    {
        string path = "Assets/All Race Data/";
        string assetPathAndName = "";
#if UNITY_EDITOR
        assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + typeof(RaceData).ToString() + ".asset");
#endif
        return assetPathAndName;
    }

    public void ClearLevel()
    {
        Destroy(startLine);

        foreach (GameObject gameObject in checkPoints)
        {
            Destroy(gameObject);
        }

        checkPoints.Clear();

        Destroy(endLine);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Debug.DrawLine(startLine.transform.position, checkPoints[0].transform.position);

        for (int i = 0; i < checkPoints.Count - 1; i++)
        {
            Debug.DrawLine(checkPoints[i].transform.position, checkPoints[i + 1].transform.position);
        }

        Debug.DrawLine(endLine.transform.position, checkPoints[checkPoints.Count - 1].transform.position);
    }

}
