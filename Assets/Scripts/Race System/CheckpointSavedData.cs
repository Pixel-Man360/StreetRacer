using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointSavedData
{
    public Vector3 position;
    public Quaternion rotation;

    public CheckpointSavedData(Vector3 pos, Quaternion rot)
    {
        this.position = pos;
        this.rotation = rot;
    }
}
