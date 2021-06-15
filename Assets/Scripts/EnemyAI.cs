using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/******************************************************************************
 * Project: GPA4300Game
 * File: EnemyAI.cs
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
 *  14.06.2021  RK  Created
 *  
 *****************************************************************************/
public class EnemyAI : MonoBehaviour
{
    [Header("UI Controls")]
    public Text destText;

    [Header("AI Controls")]
    public Transform[] destPoints;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(NextDestination().position);
    }

    // Update is called once per frame
    void Update()
    {

        // TODO: Patrouilliere
        // TODO: Suche Spieler

    }

    /// <summary>
    /// Bestimmt per Zufallzahl ein neues Ziel
    /// </summary>
    /// <returns></returns>
    private Transform NextDestination()
    {
        int rnd = Random.Range(0, destPoints.Length);
        destText.text = $"Destination: {destPoints[rnd].name}";

        return destPoints[rnd];
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

    /// <summary>
    /// Bestimmt für den Agent ein neues Ziel
    /// </summary>
    public void NextDestPoint()
    {
        agent.ResetPath();
        agent.SetDestination(NextDestination().position);
    }
}
