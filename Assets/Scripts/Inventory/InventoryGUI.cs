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
    public List<Item> inventory;

    private int inventoryMaxSize = 10;
    private int itemCount = 0;

    int scrollCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGUI();
    }
    private void Update()
    {
            SelectItem();
            UseItem();
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

    private void SelectItem()
    {
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

            if (mouseScroll > 0f && scrollCount < inventoryMaxSize-1)
            {
                UpdateGUI();
                //TODO: Anzeige + highlighten von dem Item bei scrollCount / Animation
                scrollCount++;
            }
            else if (mouseScroll < 0f && scrollCount > 0)
            {
                UpdateGUI();
                //TODO: Anzeige + highlighten von dem Item bei scrollCount / Animation
                scrollCount--;
            }
    }

    private void UseItem()
    {
        if (inventory[itemCount].itemType.Equals(IItemTypes.ItemType.Heal)){
            //TODO: Spieler heilen
            RemoveItem(inventory[itemCount]);
        }else if (inventory[itemCount].itemType.Equals(IItemTypes.ItemType.PowerUp)){
            //TODO: PowerUp aktivieren
            RemoveItem(inventory[itemCount]);
        }else if (inventory[itemCount].itemType.Equals(IItemTypes.ItemType.Quest)){
            //TODO: Hinzufügen von Quest Mechanics
            RemoveItem(inventory[itemCount]);
        }
    }



    public void UpdateGUI()
    {
        int itemCount = guiInventoryImages.Length;

        foreach (Image item in guiInventoryImages)
        {
            guiInventoryImages[itemCount].enabled = false;
        }
        int secondItemCount = 0;
        foreach (Item item in inventory)
        {
            guiInventoryImages[secondItemCount].enabled = true;
            guiInventoryImages[secondItemCount].sprite = item.sprite;
        }
    } 
}
