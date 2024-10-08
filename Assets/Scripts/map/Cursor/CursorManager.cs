using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public RectTransform hand;
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    private ItemName currentItem;
    private bool canClick;
    private bool holdItem;
    private Collider2D ObjectAtMousePosition()
    {
        //return Physics2D.OverlapPoint(mouseWorldPos);
        return Physics2D.OverlapPoint(catmove.player.position);
    }
    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }
    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }
    private void OnItemSelectedEvent(ItemDetails itemDetails,bool isSelected)
    {
        holdItem = isSelected;
        if(isSelected)
        {
            currentItem = itemDetails.itemName;
        }
        hand.gameObject.SetActive(holdItem);
    }
    private void OnItemUsedEvent(ItemName itemName)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //canClick = ObjectAtMousePosition();
        //if (hand.gameObject.activeInHierarchy)
        //{
        //    hand.position = Input.mousePosition;
        //}
        //if (canClick && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(key: KeyCode.F)))
        if (teleport.isEnter && Input.GetKeyDown(key: KeyCode.F))
        {
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }
    private void ClickAction(GameObject clickObject)
    {
        switch(clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<teleport>();
                teleport?.Teleport(); 
                break;
            case "Item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "Interactive":
                var interactive = clickObject.GetComponent<Interactive>();
                if (holdItem)
                    interactive?.CheckItem(currentItem);
                else
                    interactive?.EmptyClicked();
                break;
        }
    }
}
