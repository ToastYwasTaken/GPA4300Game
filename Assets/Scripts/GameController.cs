using UnityEngine;

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
 *  
 *****************************************************************************/
public class GameController : MonoBehaviour
{
    [SerializeField]
    private Transform[] exits;
    [SerializeField]
    private GameObject RockPilePrefab;
    void Start()
    {
        int randomExit = Random.Range(0, exits.Length);
        // Fixiert die Maus und blendet sie aus
        Cursor.lockState = CursorLockMode.Locked;
    }

}
