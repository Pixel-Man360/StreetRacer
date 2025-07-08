using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointSavedData
{
    public Vector3 position;
    public Quaternion rotation;

    public PointSavedData(Vector3 pos, Quaternion rot)
    {
        this.position = pos;
        this.rotation = rot;
    }
}
