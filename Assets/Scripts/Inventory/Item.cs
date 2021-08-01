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
 *  09.07.2021  FM  Created
 *  30.07.2021  FM  Changed to Monobehaviour, no ScriptableObject anymore
 *  01.08.2021  FM  Added Properties, changed access modifiers, added default automatic setup for name and prefabs
 *  
 *****************************************************************************/

/// <summary>
/// Items
/// Sprites shouldn't be null in inspector
/// </summary>
public class Item : MonoBehaviour
    {
    public string itemName;
    [SerializeField]
    private GameObject itemGameobjectPrefab;
    [SerializeField]
    private Sprite sprite;   //Das Sprite, das im Inventar angezeigt werden soll
    [SerializeField]
    private IItemTypes itemType;

    private void Start()
    {
        SetDefaultItemName();
        SetDefaultGameobject();
    }

    private void SetDefaultGameobject()
    {
        if(this.itemGameobjectPrefab == null)
        {
            itemGameobjectPrefab = this.gameObject;
        }
    }

    private void SetDefaultItemName()
    {
        if (this.itemName == null)
        {
            itemName = this.gameObject.name;
        }
    }

    public IItemTypes PItemType { get => itemType; }
    public Sprite PItemSprite { get => sprite; }
}
