using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
 *  11.06.2021  FM  erstellt
 *  22.06.2021  FM  health mechanic überarbeitet
 *  24.06.2021  FM  Debuglog entfernt
 *  26.06.2021  RK  LoadScene zu LoadSceneAsync geändert
 *  26.07.2021  FM  OnTriggerEnter Event hinzugefügt
 *  28.07.2021  FM  maxVal für health hinzugefügt
 *  17.08.2021  FM  Werte angepasst
 *  
 *****************************************************************************/

/// <summary>
/// Updatet die Lebenspunkte des Spielers nach Kollision
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private sbyte phealth = 100;    //Startwert
    public sbyte pdamage;   //Basisschaden des Spielers
    public GameObject enemy;
    private EnemyHealth enemyHealth;
    private sbyte edamage;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        edamage = enemyHealth.edamage;
    }

    // Update is called once per frame
    void Update()
    {
        if(phealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync(2);  //loading death screen
        }else if(phealth >= 100)
        {
            phealth = 100;
        }
    }

    private void OnCollisionEnter(Collision collision)  
    {
        if (collision.gameObject.tag.Equals("Enemy"))   //tag = "Enemy"
        {
            phealth -= edamage;
            Debug.Log(phealth);
            Debug.Log(edamage);
        }

        if (collision.gameObject.tag == "Trap")
        {
            phealth = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            phealth -= edamage;
            Debug.Log(phealth);
            Debug.Log(edamage);
        }
    }

    public sbyte pHealthProperty 
    { 
        get => phealth;
        set => phealth = value; 
    }
}
