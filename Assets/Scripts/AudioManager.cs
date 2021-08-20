using UnityEngine;

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
    private AudioSource enemySFX;
    [SerializeField]
    private AudioSource environment;

    private float audioVolume = 1f;
    public float AudioVolume
    {
        set
        {
            audioVolume = value;
            ChangedVolume(audioVolume);
        }
    }

    private bool musicMute = false;
    public bool MusicMute
    {
        set
        {
            musicMute = value;
            SwitchMuteMusic(musicMute);
        }
    }
    private bool sFXMute = false;
    public bool SFXMute
    {
        set
        {
            sFXMute = value;
            SwitchMuteSFX(sFXMute);
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

    // Start is called before the first frame update
    void Start()
    {
        AudioVolume = Preferences.instance.Load_AudioVolume();      
    }

    private void ChangedVolume(float _value)
    {
        if (enemySFX) enemySFX.volume = _value;
        if (environment) environment.volume = _value;
    }

    private void SwitchMuteMusic(bool _value)
    {
        if (environment) environment.mute = !_value;
    }
    private void SwitchMuteSFX(bool _value)
    {
        if (enemySFX) enemySFX.mute = !_value;
    }

    public void StopAllAudioSources()
    {
        if (enemySFX) enemySFX.Stop();
        if (environment) environment.Stop();
    }


    public void PlayGameSceneAudio()
    {
        if (environment) environment.Play();
    }

}
