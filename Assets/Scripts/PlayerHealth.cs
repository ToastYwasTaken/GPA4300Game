using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerHealth.cs
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
/// Updatet die Lebenspunkte des Spielers nach Kollision
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private byte phealth = 100;    //Startwert
    public GameObject edamageObject;
    private byte edamage;

    // Start is called before the first frame update
    void Start()
    {
        edamage = edamageObject.GetComponent<Damage>().eDamageProperty;
    }

    // Update is called once per frame
    void Update()
    {
        if(phealth < 0)
        {
            
        }
    }

    private void OnCollisionEnter(Collision collision)  
    {
        if (collision.gameObject.tag.Equals("Enemy"))   //tag = "Enemy"
        {
            phealth -= edamage;
        }
    }

    public byte pHealthProperty 
    { 
        get => phealth;
        set => phealth = value; 
    }
}
