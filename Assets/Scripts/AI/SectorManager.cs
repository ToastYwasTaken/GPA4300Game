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
 *  24.06.2021  RK  Created
 * 
 *  
 *****************************************************************************/

public class SectorManager : MonoBehaviour
{

    public Sector[] sectors;

    public Text sectorA;
    public Text sectorB;
    public Text sectorC;



    private void Start()
    {
        sectors = FindObjectsOfType<Sector>();
    }

    private void FixedUpdate()
    {
        sectorA.text = $"Sector A Stay Time: {sectors[0].playerStayTime}";
        sectorB.text = $"Sector B Stay Time: {sectors[1].playerStayTime}";
        sectorC.text = $"Sector C Stay Time: {sectors[2].playerStayTime}";
    }

    public Waypoint[] GetWaypointsFromSector()
    {
        if (sectors == null)
            return null;

        float longestStandingTime = sectors[0].playerStayTime;
        Sector sector = sectors[0];

        for (int i = 1; i < sectors.Length; i++)
        {
            if (longestStandingTime < sectors[i].playerStayTime)
            {
                longestStandingTime = sectors[i].playerStayTime;
                sector = sectors[i];
            }
        }

        return sector.Waypoints;
    }

}
