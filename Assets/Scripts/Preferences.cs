using UnityEngine;

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
 *  05.07.2021  RK  Created
 *  26.07.2021  RK  Added Save_PlayerPosition()
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

    public void Save_PlayerPostion(Vector3 _value)
    {
        float x, y, z;
        x = _value.x;
        y = _value.y;
        z = _value.z;

        string str = _value.ToString();

        // TODO

        Debug.Log(str);
    }
}
