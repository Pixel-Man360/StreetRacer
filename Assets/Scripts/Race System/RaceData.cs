using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Race Data")]
public class RaceData : ScriptableObject
{
    public CheckpointSavedData startingLine;
    public List<CheckpointSavedData> checkPoints;
    public CheckpointSavedData finishLine;
    public CheckpointSavedData playerStartPoint;
    public List<CheckpointSavedData> otherRacersStartPoint;
    public RaceType raceType;
}


public enum RaceType
{
    TimeAttack,
    Sprint,
    Circuit,
    Drift
}
