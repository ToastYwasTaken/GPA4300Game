using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
 *  15.07.2021  RK  Created
 *  
 *  
 *****************************************************************************/
public class UIManager : MonoBehaviour
{
    public GameObject uIPause = null;

    private void Start()
    {
        uIPause.SetActive(false);

    }

    public void ShowUIPause(bool _value)
    {
        if (uIPause)
        {
            uIPause.SetActive(_value);
        }
        else
            Debug.LogError("Kein UI: Pause festgelegt!");
      
    }


}
