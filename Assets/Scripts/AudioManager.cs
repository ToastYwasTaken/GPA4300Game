using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: AudioManager.cs
 * Version: 1.0
 * Autor: Ren� Kraus (RK); Franz M�rike (FM); Jan Pagel (JP)
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
 *  20.08.2021  RK  hinzugef�gt AudioSource enemySFX
 *                              AudioSource playerSFX
 *                              AudioSource environment
 *  
 *****************************************************************************/
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip audioClipMainMenu = null;
    public AudioClip audioClipGameScene = null;

    [SerializeField]
    private AudioSource backgroundMusic = null;
    private AudioSource enemySFX = null;
    private AudioSource playerSFX = null;
    private AudioSource environment = null;


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
        DontDestroyOnLoad(gameObject);

        backgroundMusic = GetComponent<AudioSource>();
        if (backgroundMusic)
        {
            backgroundMusic.volume = Preferences.instance.Load_AudioVolume();
        }
        else
            Debug.LogError("Kein AudioSource gefunden!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
