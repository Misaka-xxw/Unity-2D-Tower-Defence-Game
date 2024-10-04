using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;
    public bool isDone;
    public void CheckItem(ItemName itemName)
    {
        if(itemName==requireItem&&!isDone)
        {
            isDone = true;
            OnCLickedAction();
            EventHandler.CallItemUsedEvent(itemName);
        }
    }
    protected virtual void OnCLickedAction()
    {

    }
    public virtual void EmptyClicked()
    {
        Debug.Log("Empty.");
    }
}
