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
 *  09.07.2021  RK  erstellt
 *  20.08.2021  RK  hinzugefügt AudioSource enemySFX
 *                              AudioSource playerSFX
 *                              AudioSource environment
 *  
 *****************************************************************************/
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

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
    private bool masteraudioMute = false;
    public bool MasteraudioMute
    {
        get => masteraudioMute;
        set
        {
            masteraudioMute = value;
            SetMute(masteraudioMute);
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

    private void SetVolume(float _value)
    {
        float dBValue = 0f;

        if (_value <= 0)
        {
            dBValue = -80f;
        }
        else
        {
            dBValue = 100 * Mathf.Log10(_value);
            Debug.Log("Mixer Volume: " + dBValue);
        }

        masterMixer.SetFloat("masterVol", dBValue);
    }

    private void SetMute(bool _value)
    {
     
    }

    /*public void musicVolume(float muVol) {
        float wert = 0;
        if (muVol > 0.38f) {
            wert = 100 * Mathf.Log10(muVol);
        }
        else {
            wert = -80f;
        }
        MasterMixer.SetFloat("musicVol", wert);
}
    */
    #region old
    //[SerializeField]
    //private AudioSource enemySFX;
    //[SerializeField]
    //private AudioSource environment;

    //private float audioVolume = 1f;
    //public float AudioVolume
    //{
    //    set
    //    {
    //        audioVolume = value;
    //        ChangedVolume(audioVolume);
    //    }
    //}

    //private bool musicMute = false;
    //public bool MusicMute
    //{
    //    set
    //    {
    //        musicMute = value;
    //        SwitchMuteMusic(musicMute);
    //    }
    //}
    //private bool sFXMute = false;
    //public bool SFXMute
    //{
    //    set
    //    {
    //        sFXMute = value;
    //        SwitchMuteSFX(sFXMute);
    //    }
    //}


    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(gameObject);
    //    }

    //}

    // Start is called before the first frame update
    //void Start()
    //{
    //    AudioVolume = Preferences.instance.Load_AudioVolume();      
    //}

    //private void ChangedVolume(float _value)
    //{
    //    if (enemySFX) enemySFX.volume = _value;
    //    if (environment) environment.volume = _value;
    //}

    //private void SwitchMuteMusic(bool _value)
    //{
    //    if (environment) environment.mute = !_value;
    //}
    //private void SwitchMuteSFX(bool _value)
    //{
    //    if (enemySFX) enemySFX.mute = !_value;
    //}
    #endregion
}
