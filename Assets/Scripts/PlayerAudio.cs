using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerAudio.cs
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
 *  28.06.2021  RK  erstellt
 *  21.08.2021  RK  PlayerController Referenz hinzugefügt
 *              RK  Methoden im PlayerController registieren
 *  
 *****************************************************************************/
public class PlayerAudio : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField]
    AudioSource breathingSound;
    [SerializeField]
    AudioSource sighSound;
    [SerializeField]
    AudioSource painSound;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();

        playerController.SetOnCamSprint(PlayBreathingSound);
        playerController.SetOnCamIdle(StopBreathingSound);
        playerController.SetOnPlayerHit(PlayPainSound);
        playerController.SetOnPlayerResetEndurance(PlaySighSound); 
        
        
        breathingSound.loop = true;
        sighSound.loop = false;
        painSound.loop = false;

    }

    private void PlayBreathingSound()
    { 
       
        breathingSound.Play();
       
    } 
    
    private void StopBreathingSound()
    {
        breathingSound.Stop();
    }
    
    private void PlaySighSound()
    {
        sighSound.Play();
    }

    private void PlayPainSound()
    {
        painSound.Play();
    }

 
}
