using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: EnemyAudio.cs
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
 *  26.06.2021  RK  erstellt
 *****************************************************************************/
public class EnemyAudio : MonoBehaviour
{
    [SerializeField]
    EnemyAI enemyAI;

    [SerializeField]
    AudioSource attackSound;
    [SerializeField]
    AudioSource seePlayerSound;
    [SerializeField]
    AudioSource laughSound;


    private void Start()
    {
        if (enemyAI == null)
        {
            enemyAI = FindObjectOfType<EnemyAI>();
        }

        enemyAI.SetOnPatrol(PlayLaughSound);
        enemyAI.SetOnSeePlayer(PlaySeeYouSound);
        enemyAI.SetOnAttack(PlayAttackSound);

    }

    /// <summary>
    /// Spielt den Sound ab, wenn der Feind angreift
    /// </summary>
    private void PlayAttackSound()
    {
        if (!attackSound.isPlaying)
        {
            attackSound.Play();
        }
        
    }

    /// <summary>
    /// Spielt den Sound ab, wenn der Feind den Spieler entdeckt hat
    /// </summary>
    private void PlaySeeYouSound()
    {
        if (!seePlayerSound.isPlaying)
        {
            seePlayerSound.Play();
        }
        
    }

    /// <summary>
    /// Spielt den Sound ab, wenn der Feind einen Wegpunkt verlässt
    /// </summary>
    private void PlayLaughSound()
    {
        if (laughSound.isPlaying)
        {
            laughSound.Play();
        }
        
    }
}
