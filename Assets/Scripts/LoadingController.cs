using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/******************************************************************************
 * Project: GPA4300Game
 * File: LoadingController.cs
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
 *  06.07.2021  RK  erstellt
 *  
 *****************************************************************************/

public class LoadingController : MonoBehaviour
{

    delegate void CompletedOperation();
    public TextMeshProUGUI ProgressText;

    private AsyncOperation operation;
    private float operatinProgress = 0;

    // Start is called before the first frame update
    void Start()
    {   
        ProgressText.text = $"Lade: {0} %";

        StartCoroutine(LoadingGame());
    }


    private void Operation_completed(AsyncOperation obj)
    {
        Debug.Log(obj.isDone);
        Debug.Log("Scene loading completed");
       // obj.allowSceneActivation = true;
    }

    public void LoadGame()
    {
       // StartCoroutine(LoadingGame());
    }

    /// <summary>
    /// Lädt die GameScene und gibt den Fortschritt zurueck
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadingGame()
    {
        yield return null;
        operation = SceneManager.LoadSceneAsync(1);
        operation.completed += Operation_completed;

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            operatinProgress = operation.progress;

            Debug.Log(operatinProgress);
            ProgressText.text = $"Lade: {operatinProgress * 100:##0} %";

            if (operatinProgress >= 0.9f)
            {
                operation.allowSceneActivation = true;
                break;
            }
            yield return null;
        }

    }

}
