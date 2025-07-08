using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EconomyData
{
    public int coinCount;

    public EconomyData(int coinCount)
    {
        this.coinCount = coinCount;
    }
}

[System.Serializable]
public class SavedCarPos
{
    public float x;
    public float y;
    public float z;

    public SavedCarPos(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}