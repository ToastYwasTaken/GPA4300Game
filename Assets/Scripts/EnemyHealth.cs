using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: GPA4300Game
 * File: EnemyHealth.cs
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
/// Updatet die Lebenspunkte des Gegners nach Kollision
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private byte ehealth = 100;    //Startwert
    public GameObject pdamageObject;
    private byte pdamage;

    // Start is called before the first frame update
    void Start()
    {
        pdamage = pdamageObject.GetComponent<Damage>().pDamageProperty;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bullet")) //tag = "Bullet"
        {
            ehealth -= pdamage;
        }
    }

    public byte eHealthProperty
    {
        get => ehealth;
        set => ehealth = value;
    }
}
