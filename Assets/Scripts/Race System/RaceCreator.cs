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

    [Space]
    [Space]
    [Space]

    public RaceType raceType;
    public GameObject startLine;
    public List<GameObject> checkPoints;
    public GameObject endLine;
    public GameObject playStartPoint;


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
        playStartPoint = new GameObject("Player Start Point");
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
        data.raceType = raceType;
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

}
