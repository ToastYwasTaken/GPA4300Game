using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
/******************************************************************************
 * Project: GPA4300Game
 * File: GUIInventory.cs
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
 *  09.07.2021  FM  erstellt
 *  30.07.2021  FM  Inventarstruktur überarbeitet
 *  01.08.2021  FM  Properties hinzugefügt, bugs behoben
 *  08.08.2021  FM  GUI zeigt jetzt die richtigen Items an 
 *                  Problem: Manchmal wird ein Item öfter als 1 mal hinzugefügt
 *  12.08.2021  FM  Problem gefixed
 *  14.08.2021  FM  Kommentare hinzugefügt
 *  17.08.2021  FM  Sript von InventoryGUI zu GUIInventory umbenannt
 *  
 *****************************************************************************/

/// <summary>
/// Kümmert sich um die visuelle Anzeige des Inventars
/// und die Verwaltung der Inventarstruktur
/// Inventar besteht aus Items
/// </summary>
public class GUIInventory : MonoBehaviour
{
    public Image[] guiInventoryImages;
    private List<Item> inventory = new List<Item>();

    [SerializeField]
    private GameObject mapPart1;
    [SerializeField]
    private GameObject mapPart2;
    [SerializeField]
    private GameObject mapPart3;

    public int inventoryMaxSize = 5;
    private int itemCount = 0;

    private void Start()
    {
        mapPart1.SetActive(false);
        mapPart2.SetActive(false);
        mapPart3.SetActive(false);
    }

    private void Update()
    {
        UpdateGUI();

        GetInput();
    }


    /// <summary>
    /// Übersetzt den UserInput, damit das dementsprechende Item benutzt werden kann
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
    /// Zeigt das Inventar in der Console an
    /// </summary>
    private void DisplayInventoryDebug()
    {
        foreach(Item item in inventory)
        {
            Debug.Log(item.itemName + "\n");
        }
    }


    public void OpenMap(int _mapInt)
    {
        if (_mapInt == 1)
        {
            mapPart1.SetActive(true);
        }
        else if (_mapInt == 2)
        {
            mapPart2.SetActive(true);
        }
        else if (_mapInt == 3)
        {
            mapPart3.SetActive(true);
        }
        else return;
    }


    /// <summary>
    /// Der Spieler setzt das Item ein indem er die Taste drückt, an dessen Index das
    /// gewünschte Item liegt. Der Index wird auch in der UI angezeigt
    /// </summary>
    /// <param name="_index">index im Inventar des Items</param>
    private void UseItem(int _index)
    {
        Debug.Log("In UseItem");
        DisplayInventoryDebug();
        Debug.Log($"item in inventory at index itype: {inventory[_index].PItemType}");
        Item currentItem = inventory[_index];
        //Null Check
        if (currentItem != null){
            currentItem.Use();

        }
    }


    /// <summary>
    /// Fügt Item zum Inventar hinzu
    /// Beachtet die maximale Größe
    /// </summary>
    /// <param name="_itemToAdd">Das Item welches hinzugefügt wird</param>
    public void AddItem(Item _itemToAdd)
    {
        if (itemCount < inventoryMaxSize)
        {
            inventory.Add(_itemToAdd);
            itemCount++;
        }
        UpdateGUI();
    }

    /// <summary>
    /// Löscht das Item aus dem Inventar
    /// Wenn das Inventar leer ist, passiert nichts.
    /// </summary>
    /// <param name="_itemToRemove">Das Item welches entfernt wird</param>
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
    /// Zeigt die Items in der GUI am unteren Bildschirmrand an
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
    }

    public List<Item> PGetInventory { get => inventory; }

}
