using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad Instance;

    private SaveData[] data = new SaveData[15];
    private string saveFile;
    void Awake()
    {
        saveFile = Application.persistentDataPath + "/gamedata.json";
        Instance = this;
    }

    private void Start()
    {
        
    }

    public void SaveData(TowerSpawnAndMerge.Tile[] tiles)
    {
        if (File.Exists(saveFile))
            File.Delete(saveFile);

        for (int i = 0; i < tiles.Length; i++)
        {
            data[i].isBusy = tiles[i].isBusy;
            data[i].level = tiles[i].level;
        }

        string json = JsonHelper.ToJson(data);

        File.WriteAllText(saveFile, json);
        PlayerPrefs.SetInt("IsSaved", 1);
    }

    public SaveData[] LoadData()
    {
        TowerSpawnAndMerge.Tile[] tiles = new TowerSpawnAndMerge.Tile[15];

        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);

            data = JsonHelper.FromJson<SaveData>(fileContents);
        }

        return data;
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

[Serializable]
public struct SaveData {
    public bool isBusy;
    public int level;
}

