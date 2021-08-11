using UnityEngine;
using UnityEngine.SceneManagement;

/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerController.cs
 * Version: 1.01
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
 *  11.06.2021  RK  Created
 *  15.06.2021  RK  Modified -> void Start()
 *  01.07.2021  JP  added RandomExit()
 *  15.07.2021  RK  added NewGame()
 *                  added ContinueGame()
 *                  added PauseGame()
 *                  added LoadingSettingScene()
 *                  added ExitGame()
 *  22.07.2021  RK  Bugfix Preferences NullReferenceExpection
 *  26.07.2021  RK  added SavePlayerPosition()
 *  11.08.2021  RK  added SaveEnemyPosition()
 *****************************************************************************/
public class GameController : MonoBehaviour
{
    [SerializeField]
    private bool isGamePaused = false;
    private Vector3 currentPlayerPosition;
    private Vector3 currentEnemyPosition;


    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject enemy;

    //[SerializeField]
    //private Transform[] exits;
    //[SerializeField]
    //private GameObject RockPilePrefab;

    //private void RandomExit()
    //{
    //    int randomExit = Random.Range(0, exits.Length);
    //    for(int _i = 0; _i < exits.Length; _i++)
    //    {
    //        if (_i == randomExit)
    //        {
    //            continue;                                   //L�sst nur einen Ausgang offen,...
    //        }
    //        else
    //        {
    //            Instantiate(RockPilePrefab, exits[_i]);     //...alle anderen werden zugesch�ttet.
    //        }
    //    }
    //}

    void Start()
    {
        //RandomExit();
        isGamePaused = false;

        // Fixiert die Maus und blendet sie aus
        Cursor.lockState = CursorLockMode.Locked;


        

        
        



        //preferences = FindObjectOfType<Preferences>();

        //if (preferences)
        //{
        //    audioSource.volume = preferences.Load_AudioVolume();
        //}
        //else
        //{
        //    audioSource.volume = 1f;
        //}

    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            int scenes = SceneManager.sceneCount;
            if (scenes == 1)
            {
                
                isGamePaused = !isGamePaused;
                PauseGame(isGamePaused);
            }
        }
    }

    /// <summary>
    /// Speichert die aktuelle Posistion des Spielers im Level
    ///  </sammary>
    private void SavePlayerPosition()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        if (player)
        {
            currentPlayerPosition = player.transform.position;

            Preferences.instance.Save_PlayerPosition(currentPlayerPosition);

            Debug.Log($"Current Player Position: {currentPlayerPosition}");
        }
        else
        {
            Debug.LogError("Current Player Positions not found!");
        }
    }

    /// <summary>
    /// Speichert die aktuelle Posistion des Feindes im Level
    ///  </sammary>
    private void SaveEnemyPosition()
    {
        EnemyAI enemy = FindObjectOfType<EnemyAI>();

        if (enemy)
        {
            currentEnemyPosition = enemy.transform.position;

            Preferences.instance.Save_PlayerPosition(currentEnemyPosition);

            Debug.Log($"Current Enemy Position: {currentEnemyPosition}");
        }
        else
        {
            Debug.LogError("Current Enemy Positions not found!");
        }
    }

    /// <summary>
    /// Beginnt eines neues Spiel
    /// </summary>
    public void NewGame()
    {
        // Neues Spiel starten
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
    /// L�dt die Settings Scene in die Game Scene
    /// </summary>
    public void LoadSettingScene()
    {
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Beendet das Spiel und l�dt das Hauptmen�
    /// </summary>
    public void ExitGame()
    {
        SavePlayerPosition();
        SaveEnemyPosition();

        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }


}
