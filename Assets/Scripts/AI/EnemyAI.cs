using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/******************************************************************************
 * Project: GPA4300Game
 * File: EnemyAI.cs
 * Version: 1.02
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

/*TODO
    - Gegner soll sich am Zielpunkt umsehen
    - Animation Übergänge verbessern
    - Dynamic verbessern
 */


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
    public float patrolPause = 3f;
    public float patrolSpeed = 4f;
    public float attackPause = 1.5f;
    public float attackSpeed = 7f;
    public float attackDistance = 3f;
    public float distanceToThePatrolPoint = 0.1f;
    public float distanceToThePlayer = 2.7f;
   
    public float currentAngle;
    public float angle;
    public float lookValue = 45f;
    public float lookSpeed = 10f;
    public bool isRight = false;

    SearchPlayerAI searchAI;
    Pathfinding pathfinding;
    EnemyAnimator enemyAnim;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        searchAI = GetComponent<SearchPlayerAI>();
        pathfinding = GetComponent<Pathfinding>();
        enemyAnim = FindObjectOfType<EnemyAnimator>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        //if (agent)
        //{
        //    agent.speed = patrolSpeed;
        //    agent.updatePosition = true;
        //    SetDestination(NextDestination(), distanceToThePatrolPoint);
        //    enemyAnim.PlayIdleAnimation(true);
        //}

        currentAngle = agent.transform.rotation.eulerAngles.y;
    }

    private void FixedUpdate()
    {
        LookAround();

        if (useMouseDest)
        {
            MouseDestination();
        }

        //if (!searchAI.isPlayerDetected)
        //{
        //    agent.speed = patrolSpeed;
        //    StopCoroutine(nameof(AttackPlayer));
        //    StartCoroutine(nameof(Patrol));
        //}
        //else
        //{
        //    agent.speed = attackSpeed;
        //    StopCoroutine(nameof(Patrol));
        //    StartCoroutine(nameof(AttackPlayer));
        //    SetDestination(player.transform, distanceToThePlayer);
            
        //}
    }

    public void TestFunction()
    {
        // Zum testen von Methoden
    
    }

    /// <summary>
    /// Patrouillieren
    /// </summary>
    IEnumerator Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("Ziel erreicht!");

            enemyAnim.PlayMoveAnimation(false);
            enemyAnim.PlayRunAnimation(false);
            enemyAnim.PlayIdleAnimation(true);

            AgentStop();      
            
            // Lege neues Ziel fest
            SetDestination(NextDestination(), distanceToThePatrolPoint);

            yield return new WaitForSeconds(patrolPause);

            AgentResume();
            enemyAnim.PlayIdleAnimation(false);
            enemyAnim.PlayMoveAnimation(true);
        }
        else
        {
            enemyAnim.PlayRunAnimation(false);
        }
    }

    IEnumerator AttackPlayer()
    {
        if (agent.remainingDistance <= attackDistance)
        {
            Debug.Log("Attack");

            AgentStop();

            // Angriff durchführen
            enemyAnim.TriggerAttack(); 
            yield return new WaitForSeconds(attackPause);

            AgentResume();
        }
        else
        {
            enemyAnim.PlayMoveAnimation(false);
            enemyAnim.PlayRunAnimation(true);
        }
    }


    /// <summary>
    /// IN ARBEIT
    /// Umsehen
    /// </summary>
    private void LookAround()
    {
        float rightValue = currentAngle + lookValue;
        float leftValue = currentAngle - lookValue;

        //Debug.Log("Right value: " + rightValue);
        //Debug.Log("Left value: " + leftValue);


     

        angle = Mathf.Clamp(agent.transform.rotation.eulerAngles.y, leftValue, rightValue);


        Debug.Log(Mathf.Abs( agent.transform.rotation.eulerAngles.y));
        if (angle >= rightValue)
        {
           isRight = true;  
        }
       // Debug.Log(angle + " : " + leftValue);

        if (angle <= leftValue)
        {
           isRight = false;
        }

        if (!isRight)
        {
            // angle = Mathf.Clamp(Vector3.up.y * lookSpeed * Time.deltaTime, leftValue, rightValue);
            agent.transform.Rotate(Vector3.up * lookSpeed * Time.deltaTime);
        }
        else
        {
            // angle = Mathf.Clamp(Vector3.up.y * lookSpeed * Time.deltaTime, leftValue, rightValue);
            agent.transform.Rotate(-Vector3.up * lookSpeed * Time.deltaTime);
        }
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
        SetDestination(NextDestination(), distanceToThePatrolPoint);
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
