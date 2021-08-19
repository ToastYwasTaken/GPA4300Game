using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/******************************************************************************
 * Project: GPA4300Game
 * File: EnemyAI.cs
 * Version: 1.06
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
 *  14.06.2021  RK  erstellt
 *  18.06.2021  RK  Patrol() hinzugeügt
 *  22.06.2021  RK  AttackPlayer() hinzugefügt
 *  24.06.2021  RK  LookAround()  hinzugefügt
 *                  CalcCurrentAngle() hinzugefügt
 *                  SectorManager hinzugefügt
 *  26.06.2021  RK  LookAround() überarbeitet     
 *              RK  Setze lookAroundFlag auf false, wenn der Spieler erkannt wurde
 *  15.08.2021  RK  coroutineRunning Flag hinzugefügt      
 *  20.08.2021  RK  Eigenschaft: StartPosition hinzugefügt
 *              RK  Eigenschaft: EnemyCurrentPosition hinzugefügt
 *              RK  Eigenschaft: LastDestination hinzugefügt
 *  
 *****************************************************************************/

/*TODO
    - Animation Übergänge verbessern
    - Dynamic verbessern
    - Fehler: Animation wird nicht korrekt ausgeführt, 
              wenn sich der Enemy am Waypoint befindet und dabei auf den Player trifft
    - Fehler: AI reagiert teilweise verzögert auf den Player
 */


public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private Transform enemyTransform;

    private Action OnPatrol;
    private Action OnAttack;

    [Header("Dev Mode")]
    [SerializeField]
    private bool useMouseDest = false;

    [Header("UI Controls")]
    public Text destText;

    [Header("AI Controls")]
    public Waypoint[] Waypoints;
    public float patrolPause = 3f;
    public float patrolSpeed = 4f;
    public float attackPause = 1.5f;
    public float attackSpeed = 7f;
    public float attackDistance = 3f;
    public float distanceToTheWaypoint = 0.1f;
    public float distanceToThePlayer = 2.7f;
    public float leftLookAroundLimit = 75f;
    public float rightLookAroundLimit = 75f;
    public float lookSpeed = 60f;

    private float currentAngle;
    private float angle;
    private bool lookAroundFlag = false;
    private bool isRight = false;
    private bool coroutineRunning = false;

    SearchPlayerAI searchAI;
    Pathfinding pathfinding;
    EnemyAnimator enemyAnim;
    SectorManager sectorManager;

    public Vector3 StartPosition { get; set; }
    public Vector3 EnemyCurrentPosition { get; set; }
    public Vector3 LastDestination { get; set; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        searchAI = GetComponent<SearchPlayerAI>();
        pathfinding = GetComponent<Pathfinding>();

        enemyTransform = GetComponent<Transform>();

        enemyAnim = FindObjectOfType<EnemyAnimator>();
        sectorManager = FindObjectOfType<SectorManager>();

        player = GameObject.FindGameObjectWithTag("Player");


    }

    // Start is called before the first frame update
    void Start()
    {
        if (agent)
        {
            agent.speed = patrolSpeed;
            agent.updatePosition = true;
            SetDestination(NextDestination(), distanceToTheWaypoint);
            enemyAnim.PlayIdleAnimation(true);
        }
    }

    private void FixedUpdate()
    {
        if (useMouseDest)
        {
            MouseDestination();
        }
        else
        {
            if (!searchAI.isPlayerDetected)
            {
                agent.stoppingDistance = distanceToTheWaypoint;
                agent.speed = patrolSpeed;

                
                if (!coroutineRunning)
                {
                    StopCoroutine(nameof(AttackPlayer));
                    StartCoroutine(nameof(Patrol));
                }
               
            }
            else
            { 
                AgentResume();
                agent.speed = attackSpeed;
                lookAroundFlag = false;   
                
                SetDestination(player.transform, distanceToThePlayer);
                
                if (!coroutineRunning)
                {
                    StopCoroutine(nameof(Patrol));
                    StartCoroutine(nameof(AttackPlayer));
                }
             
            }

            // Schaut sich am Wegpunkt um
            if (lookAroundFlag && !searchAI.isPlayerDetected)
            {
                LookAround(leftLookAroundLimit, rightLookAroundLimit);
            }           
        }

        // Speichert die letzte Position der KI
        EnemyCurrentPosition = enemyTransform.position;
    }

    /// <summary>
    /// Patrouillieren
    /// </summary>
    IEnumerator Patrol()
    {
        // Gibt die Wegpunkte von dem Sektor zurück,
        // indem sich der Spieler am längsten aufhält
        // Waypoints = sectorManager.GetWaypointsFromSectorByTime();
        Waypoints = sectorManager.GetWaypointsFromSector();

        coroutineRunning = true;
        OnPatrol.Invoke();

        if (agent.remainingDistance <= agent.stoppingDistance && !searchAI.isPlayerDetected)
        {
          Debug.Log("Wegpunkt: " + agent.destination);

            enemyAnim.PlayMoveAnimation(false);
            enemyAnim.PlayRunAnimation(false);
            enemyAnim.PlayIdleAnimation(true);

            AgentStop();

            // Sieh dich um
            currentAngle = CalcCurrentAngle();
            isRight = false;
            lookAroundFlag = true;
            // Lege neues Ziel fest
            SetDestination(NextDestination(), distanceToTheWaypoint);

            yield return new WaitForSeconds(patrolPause);
            lookAroundFlag = false;
      
            enemyAnim.PlayIdleAnimation(false);
            enemyAnim.PlayMoveAnimation(true);
            AgentResume();
        }
        else
        {
            enemyAnim.PlayRunAnimation(false);
        }

        coroutineRunning = false;
    }

    /// <summary>
    /// Greif den Spieler an, wenn er in Reichweite ist
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackPlayer()
    {
        coroutineRunning = true;
        OnAttack.Invoke();

        if (agent.remainingDistance <= attackDistance && searchAI.isPlayerDetected)
        {
            Debug.Log("AI: Attack");

            // Angriff durchführen
            enemyAnim.TriggerAttack();
            yield return null;
        }
        else
        {
            enemyAnim.PlayMoveAnimation(false);
            enemyAnim.PlayIdleAnimation(false);
            enemyAnim.PlayRunAnimation(true);
        }

        coroutineRunning = false;
    }

    /// <summary>
    /// Berechnet den aktuellen Sichtwinkel
    /// </summary>
    /// <returns>aktueller Sichtwinkel</returns>
    private float CalcCurrentAngle()
    {
        angle = agent.transform.rotation.eulerAngles.y;

        if (angle > 180f)
        {
            angle -= 360f;
        }

        return angle;
    }

    /// <summary>
    /// Umsehen
    /// </summary>
    private void LookAround(float _leftLimit, float _rightLimit)
    {
        float rightValue = currentAngle + _rightLimit;
        float leftValue = currentAngle - _leftLimit;

        angle = agent.transform.rotation.eulerAngles.y;

        // Übersteigt der Winkelwert 180° Grad,
        // dann subtrahiere davon 360, um einen negativen Wert zu erhalten
        if (angle > 180f)
        {
            angle -= 360f;
        }

        // Verhindert die Unter- / Überschreitung des Winkels
        angle = Mathf.Clamp(angle, leftValue, rightValue);

        if (angle >= rightValue)
        {
            isRight = true;
        }

        if (angle <= leftValue)
        {
            isRight = false;
        }

        if (!isRight)
        {
            agent.transform.Rotate(lookSpeed * Time.deltaTime * Vector3.up);
        }
        else
        {
            agent.transform.Rotate(lookSpeed * Time.deltaTime * -Vector3.up);
        }
    }

    /// <summary>
    /// Wird ausgeführt, wenn der Feind einen Wegpunkt erreicht
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint") && !searchAI.isPlayerDetected)
        {
            Waypoint waypoint = other.gameObject.GetComponent<Waypoint>();

            leftLookAroundLimit = waypoint.enemyLeftLookAroundAngle;
            rightLookAroundLimit = waypoint.enemyRightLookAroundAngle;
            lookSpeed = waypoint.lookSpeed;
            patrolPause = waypoint.waitForSeconds;
        }
    }

    /// <summary>
    /// Spieler erkennen, wenn er zu nahe kommt
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // searchAI.isPlayerDetected = true;
            enemyAnim.TriggerAttack();
            Debug.Log("AI: Close Attack");
        }
    }

    /// <summary>
    /// Bestimmt per Zufallzahl ein neues Ziel
    /// </summary>
    /// <returns></returns>
    public Transform NextDestination()
    {
        int rnd = UnityEngine.Random.Range(0, Waypoints.Length);
        return Waypoints[rnd].transform;
    }

    /// <summary>
    /// Ziel und Abstand festlegen
    /// </summary>
    /// <param name="_destPoint"></param>
    public void SetDestination(Transform _destPoint, float _destDistance)
    {
        agent.stoppingDistance = _destDistance;
        agent.SetDestination(_destPoint.position);

        // Speichert das Ziel der KI
        LastDestination = agent.destination;

        pathfinding.SetTarget(_destPoint);

        if (destText)
        {
            destText.text = $"AI Destination: {_destPoint.name}";
        }
        
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
        SetDestination(NextDestination(), distanceToTheWaypoint);
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

    public void SetOnPatrol(Action _newFunc)
    {
        OnPatrol += _newFunc;
    }
    public void SetOnAttack(Action _newFunc)
    {
        OnAttack += _newFunc;
    }

}
