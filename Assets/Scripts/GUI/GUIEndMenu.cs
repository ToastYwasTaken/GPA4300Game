using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/******************************************************************************
* Project: GPA4300Game
* File: GUIEndMenu.cs
* Version: 1.01
* Autor: Rene Kraus (RK); Franz Moerike (FM); Jan Pagel (JP)
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
*  26.08.2021  RK    erstellt
*            
*****************************************************************************/
public class GUIEndMenu : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    private Preferences preferences;
    [SerializeField]
    private UnityEngine.UI.Image fadeImage;
    [SerializeField]
    private Color imageColor = new Color(0f, 0f, 0f, 0f);
    [SerializeField]
    private float fadeInDelay = 7f;
    [SerializeField]
    private float delayOffset = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        preferences = Preferences.instance;

        SetAudioSoucrePrefs();
        audioSource.Play();

        StartCoroutine(nameof(FadeIn));
    }

    private void SetAudioSoucrePrefs()
    {
        audioSource.volume = preferences.Load_AudioVolume();
        audioSource.mute = !preferences.Load_SoundsMute();
    }

    /// <summary>
    /// lässt nach 2.5 sek den "you died" Text langsam erscheinen
    /// </summary>
    IEnumerator FadeIn()
    {
        fadeImage.color = imageColor;

        yield return new WaitForSeconds(fadeInDelay);

        for (float i = 0f; i < 1f; i += delayOffset)
        {
            fadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, i);    
            yield return new WaitForSeconds(delayOffset);
        }

        // Lade das Hauptmenue
        LoadScene(0);
    }

    /// <summary>
    /// Lädt das Hauptmenu wenn der Button gedrückt wird
    /// </summary>
    /// <param name="_sceneIndex"></param>
    public void LoadScene(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
