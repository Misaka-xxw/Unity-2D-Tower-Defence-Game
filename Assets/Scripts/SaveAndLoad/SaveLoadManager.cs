using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting;
using System.IO;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private string jsonFolder;
    private List<iSaveable> saveableList = new List<iSaveable>();
    private Dictionary<string, GameSaveData> saveDataDic = new Dictionary<string, GameSaveData>();
    protected override void Awake()
    {
        base.Awake();
        jsonFolder = Application.persistentDataPath + "/SAVE/";
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void OnStartNewGameEvent(int obj)
    {

    }
    public void Register(iSaveable saveable)
    {
        saveableList.Add(saveable);
    }
    public void Save()
    {
        saveDataDic.Clear();
        foreach(var saveable in saveableList)
        {
            saveDataDic.Add(saveable.GetType().Name,saveable.GenerateSaveData());
        }
        var resultPath = jsonFolder + "data.sav";
        var jsonData=JsonConvert.SerializeObject(saveDataDic, Formatting.Indented);
        if(!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);
        }
        File.WriteAllText(resultPath, jsonData);
    }
    public void Load()
    {
        var resultPath = jsonFolder + "data.sav";
        if(!File.Exists(resultPath))
        {
            return;
        }
        var stringData=File.ReadAllText(resultPath);
        var jsonData=JsonConvert.DeserializeObject<Dictionary<string,GameSaveData>>(stringData);
        foreach(var saveable in saveableList)
        {
            saveable.RestoreGameData(jsonData[saveable.GetType().Name]);
        }
    }
}
