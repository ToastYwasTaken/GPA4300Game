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
    public Dictionary<int, Item> inventory = new Dictionary<int, Item>();
    private int inventoryMaxSize = 10;
    private int itemCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGUI();
    }
    private void Update()
    {
        //geht nur in UseItem, wenn das Inventar überhaupt min 1 item hat
        if (inventory.Count != 0)
        {
            UseItem();
        }
    }

    public void AddItem(Item _itemToAdd)
    {
        if (itemCount < inventoryMaxSize)
        {
            inventory.Add(itemCount, _itemToAdd);
            itemCount++;
        }
        UpdateGUI();
    }

    public void RemoveItem(Item _itemToRemove)
    {
        int idToRemove = inventory.FirstOrDefault(x => x.Value == _itemToRemove).Key;
        if (itemCount > 0)
        {
            inventory.Remove(idToRemove);
            itemCount--;
        }
        UpdateGUI();
    }

    private void UseItem()
    {
        throw new System.NotImplementedException();
        ////Heal Item benutzen mit H
        //if (Input.GetKeyDown(KeyCode.H) && inventory.ContainsKey(){

        //    RemoveItem();
        //}
        //// Quest Item benutzen mit LeftMouse
        //else if (Input.GetKeyDown(KeyCode.Mouse0) && inventory.ContainsKey()){
        //    CheckIfYouCanApplyQuestItem();
        //    RemoveItem();
        ////Power Up benutzen mit P
        //} else if (Input.GetKeyDown(KeyCode.P) && inventory.ContainsKey()){

        //    RemoveItem();
        //}
    }



    public void UpdateGUI()
    {
        int itemCount = guiInventoryImages.Length;

        foreach (Image item in guiInventoryImages)
        {
            guiInventoryImages[itemCount].enabled = false;
        }
        int secondItemCount = 0;
        foreach (KeyValuePair<Item, int> item in inventory)
        {
            guiInventoryImages[secondItemCount].enabled = true;
            guiInventoryImages[secondItemCount].sprite = item.Key.sprite;
        }
    } 
}
