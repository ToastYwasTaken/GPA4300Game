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
 *  30.07.2021  FM  Changed inventory structure
 *  01.08.2021  FM  Added Prop, fixed issues
 *  
 *****************************************************************************/

/// <summary>
/// Kümmert sich um die visuelle Anzeige des Inventars
/// Inventar besteht aus einer ID (Key) und einem Item (Value)
/// </summary>
public class InventoryGUI : MonoBehaviour
{
    public Image[] guiInventoryImages;
    //public List<Item> itemList; // 0 : Key, 1: HealPotion, 2: SprintPotion, 3: MapPart1, 4: MapPart2, 5: MapPart3
    private List<Item> inventory = new List<Item>();

    private int inventoryMaxSize = 5;
    private int itemCount = 0;

    PlayerHealth healthRef;

    sbyte healValue = 20;

    private void Start()
    {
           
    }

    private void Update()
    {
        UpdateGUI();
        GetInput();

    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            UseItem(0);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseItem(4);
        }
    }



    private void UseItem(int _index)
    {
        if(inventory[_index] = null)
        {
            return;
        }

        if (inventory[_index].PItemType.Equals(IItemTypes.ItemType.HealPotion))
        {
            healthRef.pHealthProperty += healValue;
            RemoveItem(inventory[_index]);
        }
        else if (inventory[_index].PItemType.Equals(IItemTypes.ItemType.SprintPotion))
        {

            RemoveItem(inventory[_index]);
        }
        else if (inventory[_index].PItemType.Equals(IItemTypes.ItemType.Key))
        {

            RemoveItem(inventory[_index]);
        }
        else if (inventory[_index].PItemType.Equals(IItemTypes.ItemType.MapPart1))
        {

            RemoveItem(inventory[_index]);
        }
        else if (inventory[_index].PItemType.Equals(IItemTypes.ItemType.MapPart2))
        {

            RemoveItem(inventory[_index]);
        }
        else if (inventory[_index].PItemType.Equals(IItemTypes.ItemType.MapPart3))
        {

            RemoveItem(inventory[_index]);
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

    private void UpdateGUI()    //TODO: Make items show up in GUI
    {
        int tempCount = 0;
        foreach(Image image in guiInventoryImages)
        {
            guiInventoryImages[tempCount].enabled = false;
        }
        tempCount = 0;
        foreach (Item item in inventory)
        {
            guiInventoryImages[tempCount].enabled = true;
            guiInventoryImages[tempCount].sprite = item.PItemSprite;
        }
    }

    public List<Item> PGetInventory { get => inventory; }

}
