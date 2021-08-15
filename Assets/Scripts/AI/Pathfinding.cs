using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/******************************************************************************
 * Project: GPA4300Game
 * File: Pathfinding.cs
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
 *  17.06.2021  RK  erstellt
 * 
 *  
 *****************************************************************************/
public class Pathfinding : MonoBehaviour
{
    private NavMeshPath path;
    private float elapsed = 0.0f;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        }

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }
    }
}
