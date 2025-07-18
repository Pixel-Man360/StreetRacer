using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Race Data")]
public class RaceData : ScriptableObject
{
    public int raceId;
    public PointSavedData startingLine;
    public List<PointSavedData> checkPoints;
    public PointSavedData finishLine;
    public PointSavedData playerStartPoint;
    public List<PointSavedData> otherRacersStartPoint;
    public List<PointSavedData> directionPoints;
    public RaceType raceType;
    public AreaType areaType;
    public List<int> rewardsPerPosition;

    [Space]
    [Header("For Time Attack Mode:")]
    public float raceTime;
}


public enum RaceType
{
    TimeAttack,
    Sprint,
    Circuit,
    Drift
}

public enum AreaType
{
    City,
    Desert,
    Forest
}
