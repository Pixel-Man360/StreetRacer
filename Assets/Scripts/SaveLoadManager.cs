using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager
{
    private const string economyDatafileName = "/economy.dat";
    private const string carposDataFileName = "/carposdat";


    #region ECONOMY_DATA
    public static void SaveEconomyData(EconomyData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + economyDatafileName, FileMode.Create);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static EconomyData LoadEconomyData()
    {
        if (File.Exists(Application.persistentDataPath + economyDatafileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + economyDatafileName, FileMode.Open);
            EconomyData data = bf.Deserialize(stream) as EconomyData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Saved EconomyData Not Found! Returning NULL!!");
            return null;
        }

    }
    #endregion

    #region CarPosData
    public static void SavedCarPosData(SavedCarPos data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + carposDataFileName, FileMode.Create);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static SavedCarPos LoadCarPosData()
    {
        if (File.Exists(Application.persistentDataPath + carposDataFileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + carposDataFileName, FileMode.Open);
            SavedCarPos data = bf.Deserialize(stream) as SavedCarPos;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Saved SavedCarPos Not Found! Returning NULL!!");
            return null;
        }
    }
    #endregion

}