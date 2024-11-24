using UnityEngine;
using UnityEngine.Audio;

/******************************************************************************
 * Project: GPA4300Game
 * File: AudioManager.cs
 * Version: 1.01
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
 *  26.07.2021  RK  erstellt
 *  
 *****************************************************************************/
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    const string MASTER_VOL = "masterVol";

    const string PLAYER_VOL = "playerVol";
    const string ENEMY_VOL = "enemyVol";
    const string SFX_VOL = "sfxVol";
    const string ENVIRONMENT_VOL = "environmentVol";

    private readonly float muteDBValue = -80f;
    private readonly float minDBValue = -60f;
    private readonly float maxDBValue = 5f;

    private float masterDBValue = 0f;
    private float playerDBValue = 0f;
    private float enemyDBValue = 0f;
    private float sfxDBValue = 0f;
    private float environmentDBValue = 0f;

    [SerializeField]
    private AudioMixer masterMixer;

    [SerializeField]
    private float masteraudioVolume = 0f;
    public float MasteraudioVolume
    {
        get => masteraudioVolume;
        set
        {
            masteraudioVolume = value;
            SetVolume(masteraudioVolume);
        }
    }

    [SerializeField]
    private bool environmentAudioMute = false;
    public bool EnvironmentAudioMute
    {
        get => environmentAudioMute;
        set
        {
            environmentAudioMute = value;
            SetEnvironmentMute(environmentAudioMute);
        }
    }

    [SerializeField]
    private bool sFXAudioMute = false;
    public bool SFXAudioMute
    {
        get => sFXAudioMute;
        set
        {
            sFXAudioMute = value;
            SetSFXMute(sFXAudioMute);
        }
    }

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
        // Speichert alle festeingestellten Werte (Editor) aus dem AudioMixer
        masterMixer.GetFloat(PLAYER_VOL, out playerDBValue);
        masterMixer.GetFloat(ENEMY_VOL, out enemyDBValue);
        masterMixer.GetFloat(SFX_VOL, out sfxDBValue);
        masterMixer.GetFloat(ENVIRONMENT_VOL, out environmentDBValue);

        // Lade die aktuellen Werte aus den PlayerPrefs
        SetVolume(Preferences.instance.Load_AudioVolume());
        SetSFXMute(!Preferences.instance.Load_SoundsMute());
        SetEnvironmentMute(!Preferences.instance.Load_AudioMute());

        //Debug.Log("Player: " + playerDBValue);
        //Debug.Log("Enemy: " + enemyDBValue);
        //Debug.Log("SFX: " + sfxDBValue);
        //Debug.Log("Environment: " + environmentDBValue);
    }

    /// <summary>
    /// Bestimmt das Master Volume des AudioMixer
    /// </summary>
    /// <param name="_value"></param>
    private void SetVolume(float _value)
    {
        masterDBValue = Mathf.Lerp(minDBValue, maxDBValue, Mathf.Clamp01(_value));
       // Debug.Log("Master: " + masterDBValue);
        masterMixer.SetFloat(MASTER_VOL, masterDBValue);
    }

    /// <summary>
    /// Setzt alle SFX Werte auf den niedrigsten möglichen Wert
    /// </summary>
    /// <param name="_value"></param>
    private void SetSFXMute(bool _value)
    {
        if (_value)
        {
            masterMixer.SetFloat(PLAYER_VOL, muteDBValue);
            masterMixer.SetFloat(ENEMY_VOL, muteDBValue);
            masterMixer.SetFloat(SFX_VOL, muteDBValue);
        }
        else
        {
            masterMixer.SetFloat(PLAYER_VOL, playerDBValue);
            masterMixer.SetFloat(ENEMY_VOL, enemyDBValue);
            masterMixer.SetFloat(SFX_VOL, sfxDBValue);
        }

    }

    /// <summary>
    /// Setzt alle Environment Werte auf den niedrigsten möglichen Wert
    /// </summary>
    /// <param name="_value"></param>
    private void SetEnvironmentMute(bool _value)
    {
        if (_value)
        {
            masterMixer.SetFloat(ENVIRONMENT_VOL, muteDBValue);
        }
        else
        {
            masterMixer.SetFloat(ENVIRONMENT_VOL, environmentDBValue);
        }
       
    }
}
