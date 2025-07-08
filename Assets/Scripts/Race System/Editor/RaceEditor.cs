using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RaceCreator))]
public class RaceEditor : Editor
{
    RaceCreator raceCreator;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        raceCreator = (RaceCreator)target;

        if (GUILayout.Button("Create Starting Line"))
        {
            raceCreator.CreateStartingPoint();
        }

        if (GUILayout.Button("Create CheckPoint"))
        {
            raceCreator.CreateCheckPoint();
        }

        if (GUILayout.Button("Create Ending Line"))
        {
            raceCreator.CreateEndingPoint();
        }

        if (GUILayout.Button("Create Player Start Point"))
        {
            raceCreator.CreatePlayerStartPoint();
        }

        if (GUILayout.Button("Create Other Racer Start Point"))
        {
            raceCreator.CreateRacerStartPoint();
        }

        if (GUILayout.Button("Create Direction Point"))
        {
            raceCreator.CreateDirectionPoint();
        }

        if (GUILayout.Button("Save Level"))
        {
            raceCreator.SaveLevel();
        }

        if (GUILayout.Button("Clear"))
        {
            raceCreator.ClearLevel();
        }

        // if (GUILayout.Button("Load Level"))
        // {

        // }
    }

}

