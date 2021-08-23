using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: ExitBehaviour.cs
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
 *  23.08.2021  RK  erstellt
 *****************************************************************************/

public class ExitBehaviour : MonoBehaviour
{
    private double elapsedTime;
    private TimeSpan playTime;
    private string requiredPlayingTime;


    private bool playerIsPlaying = true;

    private void Start()
    {
        StartCoroutine(PlayTime());
    }

    /// <summary>
    /// Zaehlt solange die Spielzeit hoch, bis der Spieler den Trigger auslöst
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayTime()
    {
        while (playerIsPlaying)
        {
            elapsedTime += Time.deltaTime;
            playTime = TimeSpan.FromSeconds(elapsedTime);

            yield return null;
        }

        
    }

    /// <summary>
    /// Zeigt UI für das gewonnene Spiel an und übergibt die benötigte Spielzeit
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            playerIsPlaying = false;
            requiredPlayingTime = playTime.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);
            FindObjectOfType<UIManager>().ShowUIWon(true, requiredPlayingTime);
        }
       
    }


}
