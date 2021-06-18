using System.Collections;
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
    private NavMeshAgent agent;
    private GameObject player;

    [Header("Dev Mode")]
    [SerializeField]
    private bool useMouseDest = false;

    [Header("UI Controls")]
    public Text destText;

    [Header("AI Controls")]
    public Transform[] patrolPoints;
    public float pauseTime = 3f;
    public float patrolSpeed = 4f;
    public float attackSpeed = 7f;
    public float stoppingDistance = 0.1f;
    public float distanceToThePlayer = 2.7f;


    SearchPlayerAI searchAI;
    Pathfinding pathfinding;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        searchAI = GetComponent<SearchPlayerAI>();
        pathfinding = GetComponent<Pathfinding>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (agent)
        {
            agent.stoppingDistance = stoppingDistance;
            agent.speed = patrolSpeed;
            SetDestination(NextDestination(), stoppingDistance);
        }

        if (searchAI)
        {

        }
    }

    private void FixedUpdate()
    {
        if (useMouseDest)
        {
            MouseDestination();
        }

        if (!player)
        {
            Debug.LogError("Kein Spieler gefunden!");
            return;
        }

        if (!searchAI.isPlayerDetected)
        {
            agent.speed = patrolSpeed;
            Patrol();
        }
        else
        {
            agent.speed = attackSpeed;
            SetDestination(player.transform, distanceToThePlayer);
        }
    }

    /// <summary>
    /// Patrouillieren
    /// </summary>
    private void Patrol()
    {
    
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("Ziel erreicht!");

            AgentStop();
            StartCoroutine(nameof(PatrolPause));
            // Lege neues Ziel fest
            SetDestination(NextDestination(), stoppingDistance);
        }
        
    }

    IEnumerator PatrolPause()
    {
        agent.speed = 0.1f;
        yield return new WaitForSeconds(pauseTime);
        
        agent.speed = patrolSpeed;

        AgentResume();
    }

    /// <summary>
    /// IN ARBEIT
    /// Umsehen
    /// </summary>
    private void LookAround()
    {
        Quaternion currentAngle = agent.transform.localRotation.normalized;

        float angle = 0f;

        angle = Mathf.Clamp(angle, -45f, 45);
        // Debug.Log(cameraAngle);

        // Rotationswinkel zuweisen
        agent.transform.localEulerAngles = new Vector3(0, angle, 0);
    }

    /// <summary>
    /// Bestimmt per Zufallzahl ein neues Ziel
    /// </summary>
    /// <returns></returns>
    public Transform NextDestination()
    {
        int rnd = Random.Range(0, patrolPoints.Length);
        return patrolPoints[rnd];
    }

    /// <summary>
    /// Ziel und Abstand festlegen
    /// </summary>
    /// <param name="_destPoint"></param>
    public void SetDestination(Transform _destPoint, float _destDistance)
    {
        agent.stoppingDistance = _destDistance;
        agent.SetDestination(_destPoint.position);

        pathfinding.SetTarget(_destPoint);
        destText.text = $"Destination: {_destPoint.name}";
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
    /// Legt zufällig ein neues Ziel fest
    /// </summary>
    public void SetNextPatrolPoint()
    {
        SetDestination(NextDestination(), stoppingDistance);
    }

    /// <summary>
    /// Ziel durch Mausklick festlegen
    /// </summary>
    private void MouseDestination()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycast))
            {
                agent.SetDestination(raycast.point);
            }
        }
    }

    public void TestFunction()
    {
        LookAround();
    }
}
