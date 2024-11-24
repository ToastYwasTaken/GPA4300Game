using UnityEngine;
using TMPro;

/******************************************************************************
 * Project: GPA4300Game
 * File: UIManager.cs
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
 *  15.07.2021  RK  erstellt
 *  
 *  
 *****************************************************************************/
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject uIPause = null; 
    [SerializeField]
    private GameObject uIWon = null;
    [SerializeField]
    private TMP_Text uIWonPlaytimeText = null;


    private void Start()
    {
        uIPause.SetActive(false);
        uIWon.SetActive(false);

    }

    /// <summary>
    /// Zeigt das UI für die Pause an
    /// </summary>
    /// <param name="_value"></param>
    public void ShowUIPause(bool _value)
    {
        if (uIPause)
        {
            uIPause.SetActive(_value);
        }
        else
            Debug.LogError("Kein UI: Pause festgelegt!");
      
    }

    /// <summary>
    /// Zeigt das UI für das Ziel an
    /// </summary>
    /// <param name="_value"></param>
    /// <param name="_playtime"></param>
    public void ShowUIWon(bool _value, string _playtime)
    {
        if (uIWon)
        {
            Cursor.lockState = CursorLockMode.None;
            uIWonPlaytimeText.text =$"Benoetigte Zeit:\n{_playtime}";
            uIWon.SetActive(_value);
        }
        else
            Debug.LogError("Kein UI: Gewonnen festgelegt!");

    }


}
