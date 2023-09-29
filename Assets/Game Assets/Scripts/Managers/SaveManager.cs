using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static SaveData Data;

    public static void Save()
    {
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(Data));
    }

    public static void Load()
    {
        Data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("Save", ""));
        if (Data == null)
        {
            Data = new();
        }
    }
}
