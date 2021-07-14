using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: AudioManager.cs
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
 *  09.07.2021  RK  Created
 *  
 *****************************************************************************/
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip audioClipMainMenu = null;
    public AudioClip audioClipGameScene = null;

    [SerializeField]
    private AudioSource audioSource = null;



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

        audioSource = FindObjectOfType<AudioSource>();
        if (audioSource)
        {
            audioSource.volume = FindObjectOfType<Preferences>().Load_AudioVolume();
        }
        else
            Debug.LogError("Kein AudioSource gefunden!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
