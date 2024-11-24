using UnityEngine;
/******************************************************************************
 * Project: GPA4300Game
 * File: ItemRotation.cs
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
 *  08.08.2021  FM  erstellt
 *  25.08.2021  FM  rotationSpeed angepasst
 *  
 *****************************************************************************/
/// <summary>
/// Simples Rotationsskript für die Items
/// </summary>
public class ItemRotation : MonoBehaviour
{
    private float speedMultiplier = 2f;
    private float tiltY = 60f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, tiltY * speedMultiplier * Time.deltaTime, 0, Space.World);
    }
}
