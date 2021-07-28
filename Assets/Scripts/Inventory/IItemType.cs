using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: IItemType.cs
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
 *  28.07.2021  FM  Updated types
 *  
 *****************************************************************************/
public interface IItemTypes
{ 
    public enum ItemType { Key, HealPotion, SprintPotion, MapPart1, MapPart2, MapPart3}
}

