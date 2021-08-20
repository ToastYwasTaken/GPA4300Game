using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/******************************************************************************
 * Project: GPA4300Game
 * File: GUIMainMenu.cs
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
 *  24.06.2021  RK  erstellt
 *  21.08.2021  RK  AudioSource hinzugefügt
 *  
 *****************************************************************************/
public class GUIMainMenu : MonoBehaviour
{
    [SerializeField]
    private Animation wizardAnim;

    public Canvas canvas;  
    public AudioSource mainSceneAudio;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
        mainSceneAudio.mute = !Preferences.instance.Load_AudioMute();
        mainSceneAudio.volume = Preferences.instance.Load_AudioVolume();
    }

    public void LoadScene(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    public void LoadGame()
    {
        wizardAnim.Stop();
        canvas.enabled = false;
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
    }


    public void LoadSettingScene()
    {
        canvas.enabled = false;
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
