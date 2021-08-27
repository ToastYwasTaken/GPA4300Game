using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: Waypoints.cs
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
 *  24.06.2021  RK  erstellt
 * 
 *  
 *****************************************************************************/

public class Waypoint : MonoBehaviour
{
    public float enemyLeftLookAroundAngle = 0f;

    public float enemyRightLookAroundAngle = 0f;

    public float lookSpeed = 60f;

    public float waitForSeconds = 3f;
}
