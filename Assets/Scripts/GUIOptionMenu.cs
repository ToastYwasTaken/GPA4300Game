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
 *  
 *****************************************************************************/

/// <summary>
/// Opens the pause / options menu
/// </summary>
 //TODO
public class GUIOptionMenu : MonoBehaviour
{

    public static bool isPaused = false;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& !isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.eulerAngles.z);
            Debug.Log("In Pause");
            if (buttonExitToDesktop.onClick.Equals(true))
            {
                Debug.Log("clicked exit to Desktop");
            }else if (buttonExitToMainMenu.onClick.Equals(true))
            {
                Debug.Log("clicked exit to Main Menu");
            }

            if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
                Debug.Log("Exited Pause");
            }
        }
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void Quit()
    {
        Application.Quit();
    }


}
