using UnityEngine;
using UnityEngine.UI;

/******************************************************************************
 * Project: GPA4300Game
 * File: SectorManager.cs
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

public class SectorManager : MonoBehaviour
{
    [SerializeField]
    private Sector[] sectors;

    private void Start()
    {
        sectors = FindObjectsOfType<Sector>();
    }

    /// <summary>
    /// Gibt die Wegpunkte des Sektors zuruek, in dem sich der Spieler 
    /// aktuell aufhält
    /// </summary>
    /// <returns></returns>
    public Waypoint[] GetWaypointsFromSector()
    {
        if (sectors == null) return null;

        foreach (Sector item in sectors)
        {
            if (item.isPlayerDetected)
            {
                return item.Waypoints;
            }
        }

        return sectors[0].Waypoints;

    }

}
