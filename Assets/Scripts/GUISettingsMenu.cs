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
 *  
 *****************************************************************************/
public class GUISettingsMenu : MonoBehaviour
{
    private GUIMainMenu mainMenu;
    private Preferences preferences;


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
        preferences = GetComponent<Preferences>();

        sensitivityValue = preferences.Load_Sensitivity();
        volumeValue = preferences.Load_AudioVolume();

        audioMute = preferences.Load_AudioMute();
        soundsMute = preferences.Load_SoundsMute();

        SetUIValue();

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

    public void ChangeSensitivityValue()
    {
        sensitivityValue = sensitivitySlider.value;
        sensitivityText.text = $"Sensitivity: {sensitivityValue:0.#}";
    }

    public void ChangeVolumeValue()
    {
        volumeValue = volumeSlider.value;
        volumeText.text = $"Volume: {volumeValue * 100:0}";
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
    }
}
