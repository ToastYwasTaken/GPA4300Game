using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/******************************************************************************
 * Project: GPA4300Game
 * File: GUISettingsMenu.cs
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
 *  05.07.2021  RK  Created
 *  14.07.2021  FM  Added functionality for pausing / accessing settings when game is running // added changing flag for option menu
 *  15.07.2021  RK  Added AudioSource
 *              RK  Added PlayerController
 *  
 *  
 *****************************************************************************/
public class GUISettingsMenu : MonoBehaviour
{
    private GUIMainMenu mainMenu;
    private Preferences preferences;
    private PlayerController playerController;

    public AudioSource audioSource;
    public Camera mainCamera;

    private float sensitivityValue = 1;
    private float volumeValue = 1;
    private bool audioMute = false;
    private bool soundsMute = false;

    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI sensitivityText;
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public Toggle audioToggle;
    public Toggle soundsToggle;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        preferences = FindObjectOfType<Preferences>();

        sensitivityValue = preferences.Load_Sensitivity();
        volumeValue = preferences.Load_AudioVolume();

        audioMute = preferences.Load_AudioMute();
        soundsMute = preferences.Load_SoundsMute();

        SetUIValue();

        Scene scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 0)
        {
            Debug.Log("Scene: " + scene.name);
            mainCamera.transform.position = new Vector3(0f, 0f, 0f);
        }
        else if (scene.buildIndex == 1)
        {
            Debug.Log("Scene: " + scene.name);
            playerController = FindObjectOfType<PlayerController>();
            mainCamera.transform.position = new Vector3(19f, 5f, 10f);
        }

    }

    private void Update()
    {
    //if (Input.GetKeyDown(KeyCode.Escape) && GUIOptionMenu.pauseFlag)    //RESUMING
    //   {
    //        Cursor.visible = false;
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Time.timeScale = 1;
    //        UnloadScene(3);
    //   }
    }

    private void SetUIValue()
    {
        sensitivityText.text = $"Sensitivity: {sensitivityValue:0.#}";
        volumeText.text = $"Volume: {volumeValue * 100:0}";

        sensitivitySlider.value = sensitivityValue;
        volumeSlider.value = volumeValue;

        audioToggle.isOn = audioMute;
        soundsToggle.isOn = soundsMute;
    }

    public void ChangeSensitivityValue(float _value)
    {
        sensitivityValue = _value;
        sensitivityText.text = $"Sensitivity: {sensitivityValue:0.#}";     

        if (playerController)
        {
            playerController.sensitivityMultiplier = _value;
        }

    }

    public void ChangeVolumeValue(float _value)
    {
        volumeValue = _value;
        volumeText.text = $"Volume: {volumeValue * 100:0}";

        if (audioSource)
        {
            audioSource.volume = _value;
        }
    }

    /// <summary>
    /// Speichert alle Einstellungen
    /// </summary>
    private void SaveAll()
    {
        audioMute = audioToggle.isOn;
        soundsMute = soundsToggle.isOn;

        preferences.Save_AudioMute(audioMute);
        preferences.Save_SoundsMute(soundsMute);
        preferences.Save_Sensitivity(sensitivityValue);
        preferences.Save_AudioVolume(volumeValue);
        preferences.SavePrefs();
    }


    public void UnloadScene(int _unloadSceneIndex)
    {
        SaveAll();

        SceneManager.UnloadSceneAsync(_unloadSceneIndex);
        mainMenu = FindObjectOfType<GUIMainMenu>();

        if (mainMenu)
        {
            mainMenu.canvas.enabled = true;
        } 
        //else if (GUIOptionMenu.pauseFlag) //changes pause flag when unloading settings menu scene
        //{
        //    GUIOptionMenu.pauseFlag = false;
        //}
    }
}
