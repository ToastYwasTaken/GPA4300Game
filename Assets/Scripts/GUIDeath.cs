using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/******************************************************************************
* Project: GPA4300Game
* File: GUIDeath.cs
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
*  11.06.2021  FM  erstellt
*  14.08.2021  FM  kommentiert
*  15.08.2021  RK  LoadScene() hinzugefügt
*              RK  mainMenuButton hinzugeüft 
*            
*****************************************************************************/

/// <summary>
/// Sorgt für die Animation nachdem der Spieler stirbt / verliert
/// </summary>
public class GUIDeath : MonoBehaviour
{   
    [SerializeField]
    private TextMeshProUGUI textOnDeath;
    [SerializeField]
    private GameObject mainMenuButton;
    [SerializeField]
    private AudioClip jumpscareClip;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuButton.SetActive(false);
        AudioSource.PlayClipAtPoint(jumpscareClip, this.transform.position);
        StartCoroutine(nameof(FadeIn));
    }

    /// <summary>
    /// lässt nach 2.5 sek den "you died" Text langsam erscheinen
    /// </summary>
    IEnumerator FadeIn()
    {
        Color textColor;
        textColor = new Color(textOnDeath.color.r, textOnDeath.color.g, textOnDeath.color.b, 0f);
        textOnDeath.color = textColor;
        //Verzögerung um 2.5 sek
        yield return new WaitForSeconds(2.5f);
        for (float i = 0f; i < 1f; i+= 0.05f)
        {
            textColor = new Color
                (textOnDeath.color.r, textOnDeath.color.g, textOnDeath.color.b, i);
            textOnDeath.color = textColor;
        //Verzögerung um 0.05 sek
            yield return new WaitForSeconds(0.05f);
        }

        mainMenuButton.SetActive(true);
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
