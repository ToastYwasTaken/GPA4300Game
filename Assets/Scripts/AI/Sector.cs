using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: Sector.cs
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
 *  24.06.2021  RK  Created
 * 
 *  
 *****************************************************************************/

public class Sector : MonoBehaviour
{

    public Waypoint[] Waypoints;

    public float playerStayTime = 0f;

    public bool isPlayerDetected;

    // Start is called before the first frame update
    void Start()
    {
        playerStayTime = 0f;
        isPlayerDetected = false;

        Waypoints = GetComponentsInChildren<Waypoint>();
    }

    private void FixedUpdate()
    {
        if (isPlayerDetected)
        {
            playerStayTime += Time.deltaTime;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerDetected = false;
    }

}
