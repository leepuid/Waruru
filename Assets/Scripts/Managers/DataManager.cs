using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    public int BestScore
    {
        get => PlayerPrefs.GetInt("BestScore", 0);
        set => PlayerPrefs.SetInt("BestScore", value);
    }

    public int Money
    {
        get => PlayerPrefs.GetInt("Money", 0);
        set => PlayerPrefs.SetInt("Money", value);
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }
}