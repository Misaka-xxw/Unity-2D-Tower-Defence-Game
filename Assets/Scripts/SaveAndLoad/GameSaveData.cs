using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveData
{
    public string currentScene;
    //public Dictionary<string, bool> miniGameStateDic;
    public Dictionary<ItemName, bool> itemAvailableDic;
    public Dictionary<string, bool> interactiveStateDic;
    public List<ItemName> itemList;
}