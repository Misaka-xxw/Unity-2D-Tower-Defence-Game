using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour,iSaveable
{
    private Dictionary<ItemName, bool> itemAvailableDic = new Dictionary<ItemName, bool>();
    private Dictionary<string, bool> interactiveStateDic = new Dictionary<string, bool>();
    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneUnloadEvent;
    }
    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneUnloadEvent;
    }
    private void OnBeforeSceneUnloadEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDic.ContainsKey(item.itemName))
                itemAvailableDic.Add(item.itemName, true);
        }
        foreach(var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDic.ContainsKey(item.name))
                interactiveStateDic[item.name] = item.isDone;
            else
                interactiveStateDic.Add(item.name, item.isDone);
        }
    }
    private void OnAfterSceneUnloadEvent()
    {
        foreach(var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDic.ContainsKey(item.itemName))
                itemAvailableDic.Add(item.itemName, true);
            else
                item.gameObject.SetActive(itemAvailableDic[item.itemName]);
        }
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDic.ContainsKey(item.name))
                item.isDone = interactiveStateDic[item.name];
            else
                interactiveStateDic.Add(item.name, item.isDone);
        }
    }
    private void OnUpdateUIEvent(ItemDetails itemDetails,int args2)
    {
        if(itemDetails!=null)
        {
            itemAvailableDic[itemDetails.itemName] = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        iSaveable saveable = this;
        saveable.SaveableRegister();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemAvailableDic = this.itemAvailableDic;
        saveData.interactiveStateDic = this.interactiveStateDic;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemAvailableDic = saveData.itemAvailableDic;
        this.interactiveStateDic = saveData.interactiveStateDic;
    }
}
