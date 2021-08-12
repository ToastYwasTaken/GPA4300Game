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
 *  08.08.2021  FM  Fixed GUI displaying correct items, still having an issue causing items to appear twice
 *  12.08.2021  FM  Fixed previous issue
 *  
 *****************************************************************************/

/// <summary>
/// Kümmert sich um die visuelle Anzeige des Inventars
/// Inventar besteht aus einer ID (Key) und einem Item (Value)
/// </summary>
public class InventoryGUI : MonoBehaviour
{
    public Image[] guiInventoryImages;
    private List<Item> inventory = new List<Item>();

    public int inventoryMaxSize = 5;
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

    /// <summary>
    /// Translates the users input number so he can use the item he wants to apply
    /// </summary>
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


    /// <summary>
    /// User can apply an Item by pressing the number of the slot where that item is located
    /// </summary>
    /// <param name="_index">index in inventory of the item to use</param>
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


    /// <summary>
    /// Shows items accordingly in GUI
    /// </summary>
    private void UpdateGUI()    
    {

        for (int i = 0; i < guiInventoryImages.Count(); i++) 
        { 
        guiInventoryImages[i].enabled = false;
        }
        
        for(int i = 0; i < inventory.Count(); i++)
        {
            guiInventoryImages[i].enabled = true;
            guiInventoryImages[i].sprite = inventory[i].PItemSprite;
        }
        int index = 0;
        //foreach(Item item in inventory)
        //{
        //    Debug.Log("Item at index " + index + ", name: " + item.itemName);
        //    index++;
        //}
    }

    public List<Item> PGetInventory { get => inventory; }

}
