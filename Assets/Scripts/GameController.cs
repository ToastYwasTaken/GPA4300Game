using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerController.cs
 * Version: 1.01
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
 *  11.06.2021  RK  Created
 *  15.06.2021  RK  Modified -> void Start()
 *  01.07.2021  JP  added RandomExit()
 *  
 *****************************************************************************/
public class GameController : MonoBehaviour
{
    [SerializeField]
    private Transform[] exits;
    [SerializeField]
    private GameObject RockPilePrefab;

    private void RandomExit()
    {
        int randomExit = Random.Range(0, exits.Length);
        for(int _i = 0; _i < exits.Length; _i++)
        {
            if (_i == randomExit)
            {
                continue;
            }
            else
            {
                /*GameObject RockPile = */Instantiate(RockPilePrefab, exits[_i]);
            }
        }
    }

    void Start()
    {
        RandomExit();
        // Fixiert die Maus und blendet sie aus
        Cursor.lockState = CursorLockMode.Locked;
    }

}
