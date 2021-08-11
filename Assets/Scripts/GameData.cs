using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/******************************************************************************
 * Project: GPA4300Game
 * File: Item.cs
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
 *  11.08.2021  
 *  
 *****************************************************************************/
public class GameData : MonoBehaviour
{
    public static GameData instance;

    Preferences preferences;

    public float Sensitivity { get; private set; } = 1f;
    public float MusicVolume { get; private set; } = 1f;
    public bool MusicMute { get; private set; } = false;
    public bool SFXMute { get; private set; } = false;


    public Vector3 PlayerPosition { get; set; } = new Vector3(0f, 0f, 0f);
    public Vector3 PlayerStartPosition { get; set; } = new Vector3(0f, 0f, 0f);
    public int PlayerHealth { get; set; } = 100;

    public Vector3 EnemyPosition { get; set; } = new Vector3(0f, 0f, 0f);
    public Vector3 EnemyStartPosition { get; set; } = new Vector3(0f, 0f, 0f);


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

       
    }

    private void Start()
    {
        preferences = FindObjectOfType<Preferences>();

        if (!preferences)
            Load_Preferences();
    }

    private void Load_Preferences()
    {

        Sensitivity = preferences.Load_Sensitivity();
        MusicVolume = preferences.Load_AudioVolume();
        MusicMute = preferences.Load_AudioMute();
        SFXMute = preferences.Load_SoundsMute();

     
    }

    public void NewGame()
    {
        PlayerPosition = PlayerStartPosition;
        EnemyPosition = EnemyStartPosition;
    }

    public void ContinueGame()
    {
        PlayerPosition = preferences.Load_PlayerPosition();
        PlayerHealth = preferences.Load_PlayerHealth();
        EnemyPosition = preferences.Load_EnemyPosition();
    }

}
