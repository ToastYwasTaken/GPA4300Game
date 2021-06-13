using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: GPA4300Game
 * File: Damage.cs
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
 *  11.06.2021  FM  Created
 *  
 *****************************************************************************/

/// <summary>
/// Der Schaden, den der Player verursacht, bzw die Kugeln
/// Der Schaden, den der Enemy am Player verursacht
/// </summary>
public class Damage : MonoBehaviour
{ 
    [SerializeField]
    private float edamage = 20f;    //Schaden des Gegners am Spieler

    [SerializeField]
    private float pdamage = 20f;   //Schaden des Spielers an Gegnern

    public float pDamageProperty 
    { 
        get => pdamage;
        set => pdamage = value; 
    }
    public float eDamageProperty
    {
        get => edamage;
        set => edamage = value;
    }
}
