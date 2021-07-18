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
 *  
 *****************************************************************************/

/// <summary>
/// ScriptableObject Item-Template für das Inventar
/// Neue Items können nun im Inspector instanziiert werden
/// </summary>
[CreateAssetMenu(fileName = "new Inventory Item")]
public class Item : ScriptableObject
    {

    public string itemName;
    public GameObject ItemGameobjectPrefab;
    public Sprite sprite;   //Das Sprite, das im Inventar angezeigt werden soll
    public IItemTypes itemType;
}
