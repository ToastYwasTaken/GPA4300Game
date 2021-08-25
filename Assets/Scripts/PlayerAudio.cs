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
    AudioSource inhalesSound;
    [SerializeField]
    AudioSource damageSound;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();

        playerController.SetOnPlayerSprint(PlayBreathingSound);
        playerController.SetOnPlayerResetEnduranceCompleted(StopBreathingSound);     
        playerController.SetOnPlayerEnduranceLimitReached(PlayInhalesSound); 

        playerController.SetOnPlayerHit(PlayTakeDamageSound);
        
        breathingSound.loop = true;
        inhalesSound.loop = false;
        damageSound.loop = false;

    }

    private void PlayBreathingSound()
    {     
        breathingSound.Play();     
    } 
    
    private void StopBreathingSound()
    {
        breathingSound.Stop();
    }
    
    private void PlayInhalesSound()
    {
        inhalesSound.Play();
    }

    private void PlayTakeDamageSound()
    {
        damageSound.Play();
    }
}
