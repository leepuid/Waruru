using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    Data data;
    string path;
    void ExistCheck(string fileName)
    {
        // 데이터 폴더의 파일 경로
        path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
            LoadData();
        else
            InitData();
    }
    public void InitData()
    {
        data = new Data();
        var result = JsonConvert.SerializeObject(data);
        Debug.Log(result);
        File.WriteAllText(path, result);
    }
    public void LoadData()
    {
        string JsonFile;
        if (File.Exists(path))
        {
            JsonFile = File.ReadAllText(path);
            Debug.Log(JsonFile);
            data = JsonConvert.DeserializeObject<Data>(JsonFile);
        }
    }
    public void SaveData()
    {
        var result = JsonConvert.SerializeObject(data);
        Debug.Log(result);
        File.WriteAllText(path, result);
    }
}

[System.Serializable]
public class Data
{
    public Data() { }
}