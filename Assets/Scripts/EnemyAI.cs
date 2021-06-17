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
    [Header("Dev Mode")]
    [SerializeField]
    private bool useMouseDest = false;

    [Header("UI Controls")]
    public Text destText;

    [Header("AI Controls")]
    public GameObject[] patrolPoints;
    public float pauseTime = 3f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SearchPlayerAI>().StartSearchingPlayer();
        agent.stoppingDistance = 0;

        SetDestination(NextDestination());

    }

    private void Update()
    {
        // TODO
    }

    private void FixedUpdate()
    {
        if (useMouseDest)
        {
            MouseDestination();
        }
        PatrolUpdate();
      //  StartCoroutine(nameof(Partrolling));
    }

    private IEnumerator Partrolling()
    {
         Debug.Log("Remaining Distance: " + agent.remainingDistance);
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("Ziel erreicht!");

            Quaternion currentAngle = agent.transform.localRotation.normalized;

            // Warte einen Moment
            yield return new WaitForSeconds(2f);
            agent.transform.localEulerAngles = new Vector3(0f, currentAngle.y + 45f, 0f);

            yield return new WaitForSeconds(2f);
            agent.transform.localEulerAngles = new Vector3(0f, currentAngle.y - 45f, 0f);

            // Warte einen Moment
            yield return new WaitForSeconds(pauseTime);
            // Lege neues Ziel fest
            SetDestination(NextDestination());
        }

    }

    private void PatrolUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("Ziel erreicht!");

            Quaternion currentAngle = agent.transform.localRotation.normalized;

            agent.transform.localEulerAngles = new Vector3(0f, currentAngle.y + 45f, 0f);

            agent.transform.localEulerAngles = new Vector3(0f, currentAngle.y - 45f, 0f);

            // Lege neues Ziel fest
            SetDestination(NextDestination());
        }
    }

    /// <summary>
    /// Bestimmt per Zufallzahl ein neues Ziel
    /// </summary>
    /// <returns></returns>
    public GameObject NextDestination()
    {
        int rnd = Random.Range(0, patrolPoints.Length);
        return patrolPoints[rnd];
    }

    /// <summary>
    /// Ziel und Abstand festlegen
    /// </summary>
    /// <param name="_destGameObject"></param>
    public void SetDestination(GameObject _destGameObject, float _destDistance)
    {
        agent.stoppingDistance = _destDistance;
        agent.SetDestination(_destGameObject.transform.position);

        destText.text = $"Destination: {_destGameObject.name}";
    }

    /// <summary>
    /// Ziel festlegen
    /// </summary>
    /// <param name="_destGameObject"></param>
    public void SetDestination(GameObject _destGameObject)
    {
        agent.stoppingDistance = 0f;
        agent.SetDestination(_destGameObject.transform.position);
        destText.text = $"Destination: {_destGameObject.name}";
    }

    /// <summary>
    /// Unterbricht die aktuelle Bewegung des Agent
    /// </summary>
    public void AgentStop()
    {
        GetComponent<SearchPlayerAI>().isPlayerDetected = false;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    /// <summary>
    /// Setzt die aktuelle Bewegung des Agent fort
    /// </summary>
    public void AgentResume()
    {
        GetComponent<SearchPlayerAI>().StartSearchingPlayer();
        agent.isStopped = false;
    }

    /// <summary>
    /// Legt zufällig ein neues Ziel fest
    /// </summary>
    public void SetNextPatrolPoint()
    {
        SetDestination(NextDestination());
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
}
