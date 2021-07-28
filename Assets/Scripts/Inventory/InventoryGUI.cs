using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
/******************************************************************************
 * Project: GPA4300Game
 * File: InventoryGUI.cs
 * Version: 1.0
 * Autor: René Kraus (RK); Franz Mörike (FM); Jan Pagel (JP)
 * 
 * 
 * These coded instructions, statements, and computer programs contain
 * proprietary information of the author and are protected by Federal
 * copyright law. They may not be disclosed to third parties or copied
 * or duplicated in any form, in whole or in part, without the prior
 * written consent of the author.
 * 
 * ChangeLog
 * ----------------------------
 *  09.07.2021  FM  Created
 *  
 *****************************************************************************/

/// <summary>
/// Kümmert sich um die visuelle Anzeige des Inventars
/// Inventar besteht aus einer ID (Key) und einem Item (Value)
/// </summary>
public class InventoryGUI : MonoBehaviour
{
    public Image[] guiInventoryImages;
    public List<Item> itemList; // 0 : Key, 1: HealPotion, 2: SprintPotion, 3: MapPart1, 4: MapPart2, 5: MapPart3
    private List<Item> inventory;

    private int inventoryMaxSize = 5;
    private int itemCount = 0;

    PlayerHealth healthRef;

    sbyte healValue = 20;

    private void Start()
    {
           
    }

    private void Update()
    {
        UpdateInventory();
        GetInput();

    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            UseItem(1);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseItem(5);
        }
    }

    private void UpdateInventory()
    {

    }

    private void UseItem(int _index)
    {
        if (inventory[_index].itemType.Equals(IItemTypes.ItemType.HealPotion))
        {

        }
        else if (inventory[_index].itemType.Equals(IItemTypes.ItemType.SprintPotion))
        {

        }
        else if (inventory[_index].itemType.Equals(IItemTypes.ItemType.Key))
        {

        }
        else if (inventory[_index].itemType.Equals(IItemTypes.ItemType.MapPart1))
        {

        }
        else if (inventory[_index].itemType.Equals(IItemTypes.ItemType.MapPart2))
        {

        }
        else if (inventory[_index].itemType.Equals(IItemTypes.ItemType.MapPart3))
        {

        }

    }


    public void AddItem(Item _itemToAdd)
    {
        if (itemCount < inventoryMaxSize)
        {
            inventory.Add(_itemToAdd);
            itemCount++;
        }
        UpdateGUI();
    }

    public void RemoveItem(Item _itemToRemove)
    {
        if (itemCount > 0)
        {
            inventory.Remove(_itemToRemove);
            itemCount--;
        }
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        int tempCount = 0;
        foreach(Image image in guiInventoryImages)
        {
            guiInventoryImages[tempCount++].enabled = false;
        }
        tempCount = 0;
        foreach (Item item in inventory)
        {
            guiInventoryImages[tempCount++].enabled = true;
            guiInventoryImages[tempCount++].sprite = item.sprite;
        }

        
    }
}
