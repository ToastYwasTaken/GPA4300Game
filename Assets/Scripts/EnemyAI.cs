using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/******************************************************************************
 * Project: GPA4300Game
 * File: EnemyAI.cs
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
 *  14.06.2021  RK  Created
 *  
 *****************************************************************************/
public class EnemyAI : MonoBehaviour
{
    public Transform destPointA;
    public Transform destPointB;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = destPointA.position;

        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destPointB.position);
    }

    // Update is called once per frame
    void Update()
    {
       



    }

    /// <summary>
    /// Unterbricht die aktuelle Bewegung des Agent
    /// </summary>
    public void AgentStop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    /// <summary>
    /// Setzt die aktuelle Bewegung des Agent fort
    /// </summary>
    public void AgentResume()
    {
        agent.isStopped = false;
    }
}
