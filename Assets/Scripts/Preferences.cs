using UnityEngine;
using System;

/******************************************************************************
 * Project: GPA4300Game
 * File: Preferences.cs
 * Version: 1.01
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
 *  05.07.2021  RK  erstellt
 *  11.08.2021  RK  Save_PlayerHealth() hinzugefügt
 *                  Load_PlayerHealth() hinzugefügt
 *  17.08.2021  FM  LoadPrefs.cs gelöscht, da Funktionalität hier gegeben
 *  21.08.2021  RK  Save_Brightness hinzugefügt
 *                  Load_Brightness hinzugefügt
 * 
 *  
 *****************************************************************************/
public class Preferences : MonoBehaviour
{
    public static Preferences instance;

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

    private Action OnPrefsSaved;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SavePrefs()
    {
        PlayerPrefs.Save();
        OnPrefsSaved?.Invoke();
    }

    #region Load

    public float Load_AudioVolume()
    {
        return PlayerPrefs.GetFloat("volume", 1f);
    }

    public float Load_Sensitivity()
    {
        return PlayerPrefs.GetFloat("sensitivity", 1f);
    }

    public bool Load_AudioMute()
    {
        return PlayerPrefs.GetInt("audio", 1) == 1;
    }

    public bool Load_SoundsMute()
    {
        return PlayerPrefs.GetInt("sounds", 1) == 1;
    }

    public float Load_Brightness()
    {
        return PlayerPrefs.GetFloat("Brightness", 0.2f);
    }

    #endregion

    #region Save

    public void Save_AudioVolume(float _value)
    {
        PlayerPrefs.SetFloat("volume", _value);
    }

    public void Save_Sensitivity(float _value)
    {
        PlayerPrefs.SetFloat("sensitivity", _value);
    }

    public void Save_AudioMute(bool _value)
    {
        PlayerPrefs.SetInt("audio", _value ? 1 : 0);
    }

    public void Save_SoundsMute(bool _value)
    {
        PlayerPrefs.SetInt("sounds", _value ? 1 : 0);
    }

    public void Save_Brightness(float _value)
    {
        PlayerPrefs.SetFloat("Brightness", _value);
    }
    #endregion

    #region Events

    public void SetOnPrefsSaved(Action _newFunc)
    {
        OnPrefsSaved += _newFunc;
    }

    #endregion
}
