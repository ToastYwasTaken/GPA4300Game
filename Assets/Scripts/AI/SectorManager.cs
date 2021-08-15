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

    public Sector[] sectors;

    public Text sectorA;
    public Text sectorB;
    public Text sectorC;
    public Text sectorD;
    public Text sectorE;

    private void Start()
    {
        sectors = FindObjectsOfType<Sector>();
    }

    private void FixedUpdate()
    {
        sectorA.text = $"Sector A Time: {sectors[0].playerStayTime} is Player: {sectors[0].isPlayerDetected}";
        sectorB.text = $"Sector B Time: {sectors[1].playerStayTime} is Player: {sectors[1].isPlayerDetected}";
        sectorC.text = $"Sector C Time: {sectors[2].playerStayTime} is Player: {sectors[2].isPlayerDetected}";
        sectorD.text = $"Sector D Time: {sectors[3].playerStayTime} is Player: {sectors[3].isPlayerDetected}";
        sectorE.text = $"Sector E Time: {sectors[4].playerStayTime} is Player: {sectors[4].isPlayerDetected}";
    }

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

    public Waypoint[] GetWaypointsFromSectorByTime()
    {
        if (sectors == null) return null;

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
