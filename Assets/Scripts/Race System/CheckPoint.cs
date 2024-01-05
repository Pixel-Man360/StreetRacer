using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private CheckPointType checkPointType;

    private int checkPointId;

    public void SetCheckPointId(int id)
    {
        id = checkPointId;
    }

    public int GetCheckPointId() => checkPointId;
}

public enum CheckPointType
{
    StartLine,
    CheckPoint,
    FinishLine
}
