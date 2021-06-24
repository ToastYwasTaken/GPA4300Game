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
 *  18.06.2021  RK  Added function          Patrol()
 *  22.06.2021  RK  Added function          AttackPlayer()
 *  24.06.2021  RK  Added function          LookAround() 
 *                  Added function          CalcCurrentAngle()
 *                  Added SectorManager
 *  
 *****************************************************************************/

/*TODO
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
    public Waypoint[] patrolPoints;
    public float patrolPause = 3f;
    public float patrolSpeed = 4f;
    public float attackPause = 1.5f;
    public float attackSpeed = 7f;
    public float attackDistance = 3f;
    public float distanceToThePatrolPoint = 0.1f;
    public float distanceToThePlayer = 2.7f;
    public float leftLookAroundLimit = 75f;
    public float rightLookAroundLimit = 75f;
    public float lookSpeed = 60f;

    private float currentAngle;
    private float angle;
    private bool lookAround = false;
    private bool isRight = false;

    SearchPlayerAI searchAI;
    Pathfinding pathfinding;
    EnemyAnimator enemyAnim;
    SectorManager sectorManager;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        searchAI = GetComponent<SearchPlayerAI>();
        pathfinding = GetComponent<Pathfinding>();
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
            SetDestination(NextDestination(), distanceToThePatrolPoint);
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
                agent.speed = patrolSpeed;
                StopCoroutine(nameof(AttackPlayer));
                StartCoroutine(nameof(Patrol));
            }
            else
            {
                agent.speed = attackSpeed;
                StopCoroutine(nameof(Patrol));
                StartCoroutine(nameof(AttackPlayer));
                SetDestination(player.transform, distanceToThePlayer);

            }

            // Schaut sich am Wegpunkt um
            if (lookAround)
            {
                LookAround(leftLookAroundLimit, rightLookAroundLimit);
            }

            // Gibt die Wegpunkte von dem Sektor zurück,
            // indem sich der Spieler am längsten aufhält
            patrolPoints = sectorManager.GetWaypointsFromSector();
        }
    }

    /// <summary>
    /// Patrouillieren
    /// </summary>
    IEnumerator Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Debug.Log("Wegpunkt: " + agent.destination);

            enemyAnim.PlayMoveAnimation(false);
            enemyAnim.PlayRunAnimation(false);
            enemyAnim.PlayIdleAnimation(true);

            AgentStop();

            // Sieh dich um
            currentAngle = CalcCurrentAngle();
            isRight = false;
            lookAround = true;

            // Lege neues Ziel fest
            SetDestination(NextDestination(), distanceToThePatrolPoint);

            yield return new WaitForSeconds(patrolPause);

            lookAround = false;
            AgentResume();
            enemyAnim.PlayIdleAnimation(false);
            enemyAnim.PlayMoveAnimation(true);
        }
        else
        {
            enemyAnim.PlayRunAnimation(false);
        }
    }

    /// <summary>
    /// Greif den Spieler an, wenn er in Reichweite ist
    /// </summary>
    /// <returns></returns>
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
            agent.transform.Rotate(Vector3.up * lookSpeed * Time.deltaTime);
        }
        else
        {
            agent.transform.Rotate(-Vector3.up * lookSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Wird ausgeführt, wenn der Feind einen Wegpunkt erreicht
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waypoint"))
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
            searchAI.isPlayerDetected = true;
        }
    }

    /// <summary>
    /// Bestimmt per Zufallzahl ein neues Ziel
    /// </summary>
    /// <returns></returns>
    public Transform NextDestination()
    {
        int rnd = Random.Range(0, patrolPoints.Length);
        return patrolPoints[rnd].transform;
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
