using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private CheckPointType checkPointType;
    private int checkPointId;

    void OnTriggerEnter(Collider other)
    {
      //  Debug.LogError(other.CompareTag("AI"));
        if (other.GetComponent<IPlayer>() != null)
        {
            RaceManager.instance.UpdateCheckPoint(this);
        }

        // else if (other.CompareTag("AI"))
        // {
        //     RaceManager.instance.OnAIEnteredCheckPoint(this, other.GetComponent<AiCarController>());
        // }
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
