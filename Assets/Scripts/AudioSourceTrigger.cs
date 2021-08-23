using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/******************************************************************************
 * Project: GPA4300Game
 * File: AudioSourceTrigger.cs
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
 *  
 *****************************************************************************/
public class AudioSourceTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    /// <summary>
    /// Löst den Clip den AudioSource aus und zerstört das GameObject danach
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                Destroy(gameObject);
                // Destroy(gameObject, audioSource.clip.length + 0.5f);
            }  
        }
    }

    
}
