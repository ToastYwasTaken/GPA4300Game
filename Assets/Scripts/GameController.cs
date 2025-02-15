using UnityEngine;
using UnityEngine.SceneManagement;

/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerController.cs
 * Version: 1.09
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
 *  11.06.2021  RK  erstellt
 *  15.06.2021  RK  Start() überarbeitet
 *  01.07.2021  JP  RandomExit() hinzugefügt
 *  15.07.2021  RK  NewGame() hinzugefügt
 *                  ContinueGame() hinzugefügt
 *                  PauseGame() hinzugefügt
 *                  LoadingSettingScene() hinzugefügt
 *                  ExitGame() hinzugefügt
 *  22.07.2021  RK  fehlerbehoben in Preferences -> NullReferenceExpection
 *  26.07.2021  RK  SavePlayerPosition() hinzugefügt
 *  11.08.2021  RK  SaveEnemyPosition() hinzugefügt
 *  17.08.2021  FM  GUIOptionMenu.cs gelöscht, Funktionalität hier eingefügt
 *  15.07.2021  FM  ButtonBehaviour.cs gelöscht, Funktionalität hier eingefügt
 *  20.08.2021  RK  
 *****************************************************************************/
public class GameController : MonoBehaviour
{
    [SerializeField]
    private bool isGamePaused = false;
    [SerializeField]
    private GameObject uIWon;

    [SerializeField]
    private EnemyAI enemyAI;

    [SerializeField]
    private Light directionalLight;

    public int PlayerHealth { get; set; } = 100;

    private float brightness = 2f;
    public float Brightness
    {
        set
        {
            brightness = value;
            SetBrightness(brightness);
        }
    }

    void Start()
    {
        //RandomExit();
        isGamePaused = false;
        // Helligkeit zum Spielstart setzen
        SetBrightness(Preferences.instance.Load_Brightness());

        if (enemyAI)
        {
            enemyAI = FindObjectOfType<EnemyAI>();
            // KI zum Anfang anhalten
            enemyAI.CanRunning = false;
        }

        // Fixiert die Maus und blendet sie aus
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Lade die End Scene, wenn true
            if (uIWon.activeInHierarchy)
            {
                GameIsWon();
                return;
            }

            int scenes = SceneManager.sceneCount;
            // Wenn nur eine Scene aktuell geladen ist, hebe die Pause auf
            if (scenes == 1)
            {

                isGamePaused = !isGamePaused;
                PauseGame(isGamePaused);
            }
        }
    }

    /// <summary>
    /// Beginnt eines neues Spiel
    /// </summary>
    public void NewGame()
    {
        // Neues Spiel starten
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    /// <summary>
    /// Spiel fortsetzen
    /// </summary>
    public void ContinueGame()
    {
        isGamePaused = false;
        PauseGame(isGamePaused);
    }

    /// <summary>
    /// Pausiert das Spiel
    /// </summary>
    /// <param name="_pause"></param>
    private void PauseGame(bool _pause)
    {
        UIManager uIManager = FindObjectOfType<UIManager>();

        if (_pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            if (uIManager)
            {
                uIManager.ShowUIPause(true);
            }
            Debug.Log("Game paused");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            if (uIManager)
            {
                uIManager.ShowUIPause(false);
            }
            Debug.Log("Game run");
        }

    }

    /// <summary>
    /// Helligkeit im Level festlegen
    /// </summary>
    /// <param name="_value"></param>
    private void SetBrightness(float _value)
    {
        directionalLight.intensity = _value;
    }

    /// <summary>
    /// Laedt die Settings Scene in die Game Scene
    /// </summary>
    public void LoadSettingScene()
    {
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Beendet das Spiel und laedt das Hauptmenue
    /// </summary>
    public void ExitGame()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    /// <summary>
    /// Beendet das Spiel und laedt die EndScene
    /// </summary>
    public void GameIsWon()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(5, LoadSceneMode.Single);
    }


}
