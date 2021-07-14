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
 *  14.07.2021  FM  minimized script, managing option menu in GUISettingsMenu.cs now
 *  
 *****************************************************************************/

/// <summary>
/// Opens the pause / options menu
/// </summary>

public class GUIOptionMenu : MonoBehaviour
{
    public static bool pauseFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !pauseFlag)   //PAUSING
        {
            pauseFlag = true;
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.eulerAngles.z); //freezing rotation
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);

        }
    }

    public static void SetPauseFlag(bool _value)
    {
        pauseFlag = _value;
    }




}
