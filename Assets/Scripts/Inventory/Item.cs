using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: GPA4300Game
 * File: Item.cs
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
 *  30.07.2021  FM  von ScriptableObject zu MonoBehaviour erbende Klasse verändert
 *                  für bessere Funktionalität
 *  01.08.2021  FM  Properties hinzugefügt, Zugriffsmodifizierer angepasst, 
 *                  automatisches Initialisieren für GO und Name hinzugefügt
 *  14.08.2021  FM  Kommentare hinzugefügt
 *  
 *****************************************************************************/

/// <summary>
/// Items
/// Sprites sollten im Inspektor gesetzt werden
/// </summary>
public class Item : MonoBehaviour
    {
    public string itemName;
    [SerializeField]
    private GameObject itemGameobjectPrefab;
    [SerializeField]
    private Sprite sprite;  //Das Sprite, das in der GUI angezeigt werden soll
    [SerializeField]
    private IItemTypes.ItemType itemType;

    private void Start()
    {
        SetDefaultItemName();
        SetDefaultGameobject();
        AssignItemType();
    }

    /// <summary>
    /// Setzt standartisierte Werte für das Gameobjekt, falls es nicht im 
    /// Inspektor initialisiert wurde
    /// </summary>
    private void SetDefaultGameobject()
    {
        if(this.itemGameobjectPrefab == null)
        {
            itemGameobjectPrefab = this.gameObject;
        }
    }

    /// <summary>
    /// Setzt standartisierte Werte für den Item-Namen, falls es nicht im 
    /// Inspektor initialisiert wurde
    /// </summary>
    private void SetDefaultItemName()
    {
        if (this.itemName == null)
        {
            itemName = this.gameObject.name;
        }
    }

    private void AssignItemType()
    {
        switch (itemName)
        {
            case "key" :
                this.PItemType = IItemTypes.ItemType.Key;
                break;
            case "healPotion" :
                this.PItemType = IItemTypes.ItemType.HealPotion;
                break;
            case "sprintPotion":
                this.PItemType = IItemTypes.ItemType.SprintPotion;
                break;
            case "map1":
                this.PItemType = IItemTypes.ItemType.MapPart1;
                break;
            case "map2":
                this.PItemType = IItemTypes.ItemType.MapPart2;
                break;
            case "map3":
                this.PItemType = IItemTypes.ItemType.MapPart3;
                break;
            default:
                break;
        }
    }
    public IItemTypes.ItemType PItemType { get => itemType; set => itemType = value; }
    public Sprite PItemSprite { get => sprite; }
}
