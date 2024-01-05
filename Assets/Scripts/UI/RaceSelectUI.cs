using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSelectUI : MonoBehaviour
{
    [SerializeField] private int raceId = 0;
    private string currentRaceHash = "Current Race Id";

    public void OnCurrentRaceClicked()
    {
        PlayerPrefs.SetInt(currentRaceHash, raceId);
    }
}
