using UnityEngine;
using CustomExtensions;

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
 *  11.08.2021  RK  Save_PlayerPosition() hinzugefügt
 *                  Load_PlayerPosition() hinzugefügt
 *                  Save_PlayerHealth() hinzugefügt
 *                  Load_PlayerHealth() hinzugefügt
 *                  Save_EnemyPosition() hinzugefügt
 *                  Load_EnemyPosition() hinzugefügt
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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }

    #region Load

    public float Load_AudioVolume()
    {
        return PlayerPrefs.GetFloat("volume");
    }

    public float Load_Sensitivity()
    {
        return PlayerPrefs.GetFloat("sensitivity");
    }

    public bool Load_AudioMute()
    {
        return PlayerPrefs.GetInt("audio") == 1;
    }

    public bool Load_SoundsMute()
    {
        return PlayerPrefs.GetInt("sounds") == 1;
    }

    public Vector3 Load_PlayerPosition()
    {
       return PlayerPrefs.GetString("PlayerPos").ToVector3();
    }

    public int Load_PlayerHealth()
    {
        return PlayerPrefs.GetInt("PlayerHealth");
    }

    public Vector3 Load_EnemyPosition()
    {
        return PlayerPrefs.GetString("EnemyPos").ToVector3();
    }

    public float Load_Brightness()
    {
        return PlayerPrefs.GetFloat("Brightness");
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

    public void Save_PlayerPosition(Vector3 _value)
    {
        PlayerPrefs.SetString("PlayerPos", _value.ToString());
    }

    public void Save_PlayerHealth(int _value)
    {
        PlayerPrefs.SetInt("PlayerHealth", _value);
    }

    public void Save_EnemyPosition(Vector3 _value)
    {
        PlayerPrefs.SetString("EnemyPos", _value.ToString());
    }

    public void Save_Brightness(float _value)
    {
        PlayerPrefs.SetFloat("Brightness", _value);
    }
    #endregion
}
