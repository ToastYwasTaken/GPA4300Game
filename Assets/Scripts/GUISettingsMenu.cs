using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/******************************************************************************
 * Project: GPA4300Game
 * File: GUISettingsMenu.cs
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
 *  05.07.2021  RK  erstellt
 *  14.07.2021  FM  Funktionalität zum Pausieren hinzugefügt 
 *                  + bool flag für Optionsmenu
 *  15.07.2021  RK  AudioSource hinzugefügt
 *              RK  PlayerController hinzugefügt
 *  22.07.2021  RK  NullReferenceExpection behoben
 *  14.08.2021  FM  auskommentierten Code und bool flag entfernt
 *  21.08.2021  RK  Helligkeitseinstellung hinzugefügt
 *  
 *  
 *****************************************************************************/
public class GUISettingsMenu : MonoBehaviour
{
    private readonly int sceneBuildIndex = 3;

    private Preferences preferences;
    private AudioManager audioManager;

    private GUIMainMenu mainMenu;
    private PlayerController playerController;
    private GameController gameController;

    [SerializeField]
    private Camera mainCamera;

    public float SensitivityValue { get; set; } = 1;
    public float VolumeValue { get; set; } = 1;
    public float LightValue { get; set; } = 2f;
    public bool AudioMute { get; set; } = false;
    public bool SoundsMute { get; set; } = false;

    [SerializeField]
    private TextMeshProUGUI volumeText;
    [SerializeField]
    private TextMeshProUGUI sensitivityText;
    [SerializeField]
    private TextMeshProUGUI lightText;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Slider sensitivitySlider;
    [SerializeField]
    private Slider lightSlider;
    [SerializeField]
    private Toggle audioToggle;
    [SerializeField]
    private Toggle soundsToggle;

    private void Start()
    {
        preferences = Preferences.instance;
        audioManager = AudioManager.instance;

        SensitivityValue = preferences.Load_Sensitivity();
        VolumeValue = preferences.Load_AudioVolume();
        AudioMute = preferences.Load_AudioMute();
        SoundsMute = preferences.Load_SoundsMute();
        LightValue = preferences.Load_Brightness();

        mainMenu = FindObjectOfType<GUIMainMenu>();
        gameController = FindObjectOfType<GameController>();

        // Zeigt die eingestellten Werte im User Interface an
        SetUIValue();

        // Gibt die ative Scene zurück
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnloadScene(sceneBuildIndex);
        }

    }


    /// <summary>
    /// Setzt die Lautstärke / Empfindlichkeit auf den vom User im Optionsmenu eingestellten Wert
    /// </summary>
    private void SetUIValue()
    {
        sensitivityText.text = $"Mausempfindlichkeit: {SensitivityValue:0.#}";
        volumeText.text = $"Lautstaerke: {VolumeValue * 100:0}";
        lightText.text = $"Helligkeit: {LightValue * 100:0}";

        sensitivitySlider.value = SensitivityValue;
        volumeSlider.value = VolumeValue;
        lightSlider.value = LightValue;
        
        audioToggle.isOn = AudioMute;
        soundsToggle.isOn = SoundsMute;
    }

    /// <summary>
    /// Ändert die Empfindlichkeit der Steuerung
    /// </summary>
    /// <param name="_value">Wert der Empfindlichkeit</param>
    public void ChangeSensitivityValue(float _value)
    {
        SensitivityValue = _value;
        sensitivityText.text = $"Mausempfindlichkeit: {SensitivityValue:0.#}";

        if (playerController)
        {
            playerController.Sensitivity = _value;
        }

    }


    /// <summary>
    /// Ändert die Lautstärke
    /// </summary>
    /// <param name="_value">Wert der Lautstärke</param>
    public void ChangeVolumeValue(float _value)
    {
        VolumeValue = _value;
        volumeText.text = $"Lautstaerke: {VolumeValue * 100:0}";

        if (audioManager)
        {
            audioManager.AudioVolume = VolumeValue;
        }

        if (mainMenu)
        {
            mainMenu.mainSceneAudio.volume = VolumeValue;
        }
    }

    /// <summary>
    /// Helligkeit einstellen
    /// </summary>
    /// <param name="_value"></param>
    public void ChangeLightValue(float _value)
    {
        LightValue = _value;
        lightText.text = $"Helligkeit: {LightValue * 100:0}";

        if (gameController)
        {
            gameController.Brightness = _value;
        }

    }

    /// <summary>
    /// schaltet die Musik / Sound stumm
    /// </summary>
    /// <param name="_value"></param>
    public void MuteMusic(bool _value)
    {
        AudioMute = _value;

        if (audioManager)
        {
            audioManager.MusicMute = AudioMute;
        }


        if (mainMenu)
        {
            mainMenu.mainSceneAudio.mute = !AudioMute;
        }

    }

    /// <summary>
    /// schaltet SFX stumm
    /// </summary>
    /// <param name="_value"></param>
    public void MuteSFX(bool _value)
    {
        SoundsMute = _value;

        if (audioManager)
        {
            audioManager.SFXMute = SoundsMute;
        }

    }

    /// <summary>
    /// Speichert alle Einstellungen
    /// </summary>
    private void SaveAll()
    {
        AudioMute = audioToggle.isOn;
        SoundsMute = soundsToggle.isOn;

        preferences.Save_AudioMute(AudioMute);
        preferences.Save_SoundsMute(SoundsMute);
        preferences.Save_Sensitivity(SensitivityValue);
        preferences.Save_AudioVolume(VolumeValue);
        preferences.Save_Brightness(LightValue);
        preferences.SavePrefs();
    }

    public void UnloadScene(int _unloadSceneIndex)
    {
        // Wenn prefernces nicht NULL ist, speicher alle Einstellungen
        if (preferences)
        {
            SaveAll();
        }

        SceneManager.UnloadSceneAsync(_unloadSceneIndex);

        if (mainMenu)
        {
            mainMenu.canvas.enabled = true;
        }
    }


}
