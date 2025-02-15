using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: StopAudioSource.cs
 * Version: 1.0
 * Autor: Ren� Kraus (RK); Franz M�rike (FM); Jan Pagel (JP)
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
public class StopAudioSource : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    /// <summary>
    /// Stop den AudioSource, wenn das GameObject zerst�rt wurde
    /// </summary>
    private void OnDestroy()
    {
        audioSource.Stop();
    }
}
