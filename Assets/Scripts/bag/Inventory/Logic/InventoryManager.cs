using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>,iSaveable
{
    public ItemDataList itemData;
    [SerializeField] private List<ItemName> itemList = new List<ItemName>();
    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ChangeItemEvent += OncChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }
    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OncChangeItemEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }
    private void OnItemUsedEvent(ItemName itemName)
    {
        var index=GetItemIndex(itemName);
        itemList.RemoveAt(index);
        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null, 1);
    }
    private void OncChangeItemEvent(int index)
    {
        if(index>=0 && index<itemList.Count)
        {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUIEvent(item, index);
        }
    }
    private void OnAfterSceneLoadedEvent()
    {
        if(itemList.Count== 0)
        {
            EventHandler.CallUpdateUIEvent(null, -1);
        }
        else
        {
            for(int i=0;i<itemList.Count; i++)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]), i);
            }
        }
    }
    public void AddItem(ItemName itemName)
    {
        if(!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
        }
    }
    private int GetItemIndex(ItemName itemName)
    {
        for(int i=0;i<itemList.Count; i++)
        {
            if (itemList[i]==itemName)
                return i;
        }
        return -1;
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
        saveData.itemList = this.itemList;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemList = saveData.itemList;
    }
}
