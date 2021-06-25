using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
/******************************************************************************
 * Project: GPA4300Game
 * File: GUIHealth.cs
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
 *  22.06.2021  FM  Created
 *  24.06.2021  FM  Fixed stuff
 *  24.06.2021  RK  Added function UnloadScene
 *  
 *****************************************************************************/

/// <summary>
/// Opens the pause / options menu
/// </summary>

public class GUIOptionMenu : MonoBehaviour
{

    public static bool isPaused = false;
    //private float volumeValue;
    //private float sensitivityValue;

    [SerializeField]
    private TextMeshProUGUI textPaused;

    [SerializeField]
    private TextMeshProUGUI textSettingVolume;
    [SerializeField]
    private Slider sliderVolume;

    [SerializeField]
    private TextMeshProUGUI textSettingSensitivity;
    [SerializeField]
    private Slider sliderSensitivity;

    [SerializeField]
    private Button buttonExitToMainMenu;

    [SerializeField]
    private Button buttonExitToDesktop;

    public PlayerController playerController;

    private bool flag = false;

    private GUIMainMenu mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        DeactivateOptionMenuGUI();
        sliderSensitivity.value = 1f;
        sliderVolume.value = 1f;
        changeVolume(1f);
        changeSensitivity(1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        { 
            changeVolume(sliderVolume.value);
            changeSensitivity(sliderSensitivity.value);
            Debug.Log("sensitivity: " + sliderSensitivity.value);
            Debug.Log("volume: " + sliderVolume.value);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)   //PAUSING
        {
            flag = true;
            ActivateOptionMenuGUI();
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y, transform.eulerAngles.z); //freezing rotation
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)    //RESUMING
        {
            flag = false;
            DeactivateOptionMenuGUI();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void DeactivateOptionMenuGUI()
    {
        //deactivate the elements of the pause menu
        textPaused.enabled = false;
        textSettingVolume.enabled = false;
        sliderVolume.gameObject.SetActive(false);
        textSettingSensitivity.enabled = false;
        sliderSensitivity.gameObject.SetActive(false);
        buttonExitToDesktop.gameObject.SetActive(false);
        buttonExitToMainMenu.gameObject.SetActive(false);

        isPaused = false;    //flag

        Time.timeScale = 1; //continues every time based function
    }

    private void ActivateOptionMenuGUI()
    {
        //activate the elements of the pause menu
        textPaused.enabled = true;
        textSettingVolume.enabled = true;
        sliderVolume.gameObject.SetActive(true);
        textSettingSensitivity.enabled = true;
        sliderSensitivity.gameObject.SetActive(true);
        buttonExitToDesktop.gameObject.SetActive(true);
        buttonExitToMainMenu.gameObject.SetActive(true);

        isPaused = true;    //flag

        Time.timeScale = 0; //makes every time based function stop //Update is still called every frame
    }

    public void changeVolume(float _sliderVolumeValue)
    {
        PlayerPrefs.SetFloat("volume", _sliderVolumeValue);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        PlayerPrefs.Save();
    }

    public void changeSensitivity(float _sliderSensitivityValue)
    {
        PlayerPrefs.SetFloat("sensitivity", _sliderSensitivityValue);
        playerController.sensitivityMultiplier = PlayerPrefs.GetFloat("sensitivity");
        PlayerPrefs.Save();
    }

    public void UnloadScene(int _unloadSceneIndex)
    {
        SceneManager.UnloadSceneAsync(_unloadSceneIndex);
        mainMenu = FindObjectOfType<GUIMainMenu>();

        if (mainMenu)
        {
            mainMenu.canvas.enabled = true;
        }
    }


}
