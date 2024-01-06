using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private CheckPointType checkPointType;
    private int checkPointId;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IPlayer>() != null)
        {
            RaceManager.instance.UpdateCheckPoint(this);
        }
    }

    public void SetCheckPointId(int id)
    {
        checkPointId = id;
    }

    public int GetCheckPointId() => checkPointId;
}

public enum CheckPointType
{
    StartLine,
    CheckPoint,
    FinishLine
}
