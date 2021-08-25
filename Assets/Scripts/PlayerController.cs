using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerController.cs
 * Version: 1.12
 * Autor: Ren� Kraus (RK); Franz M�rike (FM); Jan Pagel (JP)
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
 *  11.06.2021  RK  erstellt
 *  15.06.2021  RK  maximalen vertikalen Kamerawinkel hinzugefügt
 *  22.06.2021  FM  bool flag hinzugefügt
 *  24.06.2021  FM  Anpassungen
 *  26.06.2021  RK  Bug behoben, dass man rückwärts Sprinten konnte
 *  28.06.2021  RK  PlayerAnimator (Animations) hinzugefügt
 *  30.06.2021  RK  Collision Überprüfungen hinzugefügt
 *  09.07.2021  RK  Delegate für Sounds hinzugefügt
 *  14.07.2021  RK  Variable playerCanMove hinzugefügt
 *  22.07.2021  RK  bool flag entfernt
 *              RK  NullReferenceExpection Bug in Preferences behoben
 *  14.08.2021  FM  Kommentare angepasst
 *  20.08.2021  RK  PlayerSprint() hinzugefügt
 *  21.08.2021  FM  bool hinzugefügt für Collisionscheck Player - ExitGate
 *                  PlayerHealth.cs integriert
 *  21.08.2021  RK  Action OnPlayerIdle hinzugefügt                
 *                  Action OnPlayerJump hinzugefügt      
 *                  Action OnPlayerHit hinzugefügt      
 *                  
 *   * ChangeLog PlayerHealth.cs
 * ----------------------------
 *  11.06.2021  FM  erstellt
 *  22.06.2021  FM  health mechanic überarbeitet
 *  24.06.2021  FM  Debuglog entfernt
 *  26.06.2021  RK  LoadScene zu LoadSceneAsync geändert
 *  26.07.2021  FM  OnTriggerEnter Event hinzugefügt
 *  28.07.2021  FM  maxVal für health hinzugefügt
 *  17.08.2021  FM  Werte angepasst
 *  
 *****************************************************************************/

/*NOTES
 * Idle     0,  0.611,  0.235
 * Walk     0,  0.560,  0.280
 * Sprint   0,  0.210,  0.470
 * 
 */

public class PlayerController : MonoBehaviour
{
    //Health
    [SerializeField]
    private sbyte phealth = 100;    //Startwert
    private sbyte edamage = 20;

    //PlayerController
    private Rigidbody playerBody;

    [SerializeField]
    private Transform camTransform;

    [Tooltip("Sprint Speed")]
    [SerializeField]
    private float speedMultiplier = 2f;
    [SerializeField]
    private float maxEndurance = 10;
    [SerializeField]
    private float waitTimeForEnduranceReset = 5;
    [SerializeField]
    private float jumpForce = 200f;

    
    [SerializeField]
    private float maxVerticalCameraAngle = 45f;
    private float cameraAngle = 0f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public float fallingDownLimit = -10f;

    [SerializeField]
    private GUIInventory inventoryRef;

    // Events
    private Action OnPlayerMove;
    private Action OnPlayerSprint;
    private Action OnPlayerIdle;
    private Action OnPlayerJump;
    private Action OnPlayerHit;
    private Action OnPlayerResetEndurance;
    private Action OnPlayerResetEnduranceCompleted;
    private Action OnPlayerEnduranceLimitReached;

    //Bools
    [SerializeField]
    private bool isGrounded;
    private bool hitWall;
    public bool playerCollidingWithExitGate;

    [SerializeField]
    private bool rotatePlayerWithButtons = false;

    [SerializeField]
    private bool jumpActive = false;
    

    [SerializeField]
    private bool enduranceIsRecovery = false;

    //Properties
    //[SerializeField]
    private bool playerSprints = false;
    public bool PlayerSprints { get => playerSprints; set => playerSprints = value; } 
    [SerializeField]
    private static bool sprintActive = true;
    public static bool SprintActive {set => sprintActive = value; }

    public bool PlayerCanMove { get; set; }

    private float sensitivity = 1f;
    public float Sensitivity
    {
        set
        {
            sensitivity = value;
        }
    }
    public float Endurance { get; set; }

    public Vector3 StartPosition { get; set; } = new Vector3(0, 2, 0);

    public sbyte HealthProperty
    {
        get => phealth;
        set => phealth = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();

        //Erste Spawnposition des Spielers
        // Ausgang: 24, 2, 174
        playerBody.transform.position = StartPosition;

        OnPlayerIdle?.Invoke();

        PlayerCanMove = true;
        Endurance = maxEndurance;
    }

    // Update is called once per frame
    void Update()
    {
        // Verhindert das der Player unendlich f�llt
        FallingDownCheck();

        // Springen
        Jump();

        // Speichert die aktuelle Position des Spielers
        // PlayerCurrentPosition = playerBody.transform.position;
        UpdateHealth();
    }

    private void FixedUpdate()
    {
        PlayerRotating();

        if (PlayerCanMove) PlayerMovement();
    }

    private void UpdateHealth()
    {
        if (phealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync(2);  //death screen laden
        }
        else if (phealth >= 100)
        {
            phealth = 100;
        }
    }
    /// <summary>
    /// Setzt den Spieler zu Startposition zur�ck, wenn er f�llt
    /// </summary>
    void FallingDownCheck()
    {
        if (transform.position.y <= fallingDownLimit)
        {
            playerBody.transform.position = StartPosition;
            PlayerCanMove = true;
            sprintActive = true;
            Endurance = maxEndurance;
        }
    }

    /// <summary>
    /// Tastatur Steuerung
    /// </summary>
    void PlayerMovement()
    {
        Vector3 keyInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (keyInput == Vector3.zero)
        {
            // Idle
            OnPlayerIdle?.Invoke();
            playerSprints = false;
        }
        else
        {      
            // Bewegungsgeschwindigkeit
            float speed = moveSpeed;

            // Sprint
            if (Input.GetButton("Run") && sprintActive && keyInput.z > 0)
            {
                if (!hitWall)
                {
                    playerSprints = true;
                    speed = PlayerSprint(speed);
                }
               
            }
            else
            {
                // Walk
                speed = moveSpeed;
                playerSprints = false;

                OnPlayerMove?.Invoke();

            }

            if (!enduranceIsRecovery && Endurance < maxEndurance && !playerSprints)
            {
                StartCoroutine(ResetSprintEndurance(waitTimeForEnduranceReset, maxEndurance));
                Debug.Log("Ausdauer Wiederherstellung gestartet!");
            }

            // Mit der Tastatur drehen
            if (rotatePlayerWithButtons)
            {
                Quaternion deltaRotation = Quaternion.Euler(0, keyInput.x * rotationSpeed * Time.deltaTime, 0);
                playerBody.MoveRotation(playerBody.rotation * deltaRotation);
            }
            else
            {
                // Zur Seite gehen
                playerBody.MovePosition(playerBody.position + speed * Time.deltaTime * keyInput.x * transform.right);
            }

            // Nach vorne gehen
            playerBody.MovePosition(playerBody.position + speed * Time.deltaTime * keyInput.z * transform.forward);
        }
    }

    /// <summary>
    /// Erhöht die Geschwindigkeit des Player Unterberücksichtigung der Ausdauer
    /// </summary>
    /// <param name="_speed"></param>
    /// <returns></returns>
    float PlayerSprint(float _speed)
    {     
        if (Endurance > 0)
        {
            if (enduranceIsRecovery)
            {
                StopCoroutine(ResetSprintEndurance(waitTimeForEnduranceReset, maxEndurance));
                enduranceIsRecovery = false;
                Debug.Log("Ausdauer Wiederherstellung abgebrochen!");
            }

            waitTimeForEnduranceReset += Time.deltaTime;

            // Event aufrufen
            OnPlayerSprint?.Invoke();

            // Speed Wert erhöhen
            _speed = moveSpeed * speedMultiplier;

           

            // Ausdauer reduzieren
            ReduceSprintEndurance();

           // Debug.Log($"Ausdauer : {Endurance}");

            return _speed;
        }
        else
        {
            OnPlayerEnduranceLimitReached?.Invoke();
            
            return _speed;
        }
    }

    /// <summary>
    /// Den aktuellen Ausdauerwert reduzieren
    /// </summary>
    private void ReduceSprintEndurance()
    {
        Endurance -= 1 * Time.deltaTime;
    }

    /// <summary>
    /// Ausdauer für das sprinten zurücksetzen
    /// </summary>
    /// <param name="_waitTimeForReset"></param>
    /// <param name="_enduranceMaxLimit"></param>
    /// <returns></returns>https://onlineiemcup.com/?r=tryhardteam
    IEnumerator ResetSprintEndurance(float _waitTimeForReset, float _enduranceMaxLimit)
    {
        //Animation mit starker Atmung abspielen
        OnPlayerResetEndurance?.Invoke();

        // Ausdauer wird wiederhergestellt
        enduranceIsRecovery = true;

        Debug.Log($"Ausdauer Reset in: {_waitTimeForReset} sek.");
        yield return new WaitForSecondsRealtime(_waitTimeForReset);

        // Action aufrufen
        OnPlayerResetEnduranceCompleted?.Invoke();

        // Ausdauer auf maximum setzen
        Endurance = _enduranceMaxLimit;

        // Wiederherstellung abgeschlossen
        enduranceIsRecovery = false;

        // Die Wartezeit auf Null setzen
        waitTimeForEnduranceReset = 0f;
        Debug.Log($"Ausdauer Wiederherstellung: {Endurance}");
    }

    /// <summary>
    /// Maus Steuerung
    /// </summary>
    void PlayerRotating()
    {
        Vector3 mouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        // Spieler sieht nach oben oder unten
        cameraAngle += -mouseInput.y * rotationSpeed * sensitivity * Time.deltaTime;

        // Winkel der Kamera auf min und max begrenzen
        cameraAngle = Mathf.Clamp(cameraAngle, -maxVerticalCameraAngle, maxVerticalCameraAngle);
        // Debug.Log(cameraAngle);

        // Rotationswinkel zuweisen
        camTransform.transform.localEulerAngles = new Vector3(cameraAngle, 0, 0);

        // Spieler dreht sich nach links oder rechts
        Quaternion deltaRotationX = Quaternion.Euler(0, rotationSpeed * sensitivity * Time.deltaTime * mouseInput.x, 0);
        playerBody.MoveRotation(playerBody.rotation * deltaRotationX);
    }

    /// <summary>
    /// Springen
    /// </summary>
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && jumpActive)
        {
            OnPlayerJump?.Invoke();

            // Spring
            playerBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Hit Animation abspielen
            OnPlayerHit?.Invoke();
            Debug.Log("Player Hit!");
            //Health aktualisieren
            phealth -= edamage;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Player befindet sich auf den Boden
            isGrounded = true;
        }
        else if (collision.gameObject.tag.Equals("Enemy"))   //tag = "Enemy"
        {
            phealth -= edamage;
        }

        //Sofortiger Tod bei Falle
        else if (collision.gameObject.tag.Equals("Trap"))
        {
            phealth = 0;
        }
    }

    /// <summary>
    /// Verhindert das der Player durch Wände laufen kann
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("ExitGate"))
        {
            hitWall = true;
        }
        else
        {
            hitWall = false;
        }

        if (collision.gameObject.CompareTag("ExitGate"))
        {
            //true wenn Player im Collider des ExitGates steht
            playerCollidingWithExitGate = true;
            if (!inventoryRef.PGetInventory.Any(x => x.PItemType == IItemTypes.ItemType.Key))
            {
                    //Zeige an, dass der Spieler zum Öffnen des Tores
                    //noch einen Schlüssel finden muss
                    //TODO: add delay
                    Debug.Log("no key in inventory");
                    inventoryRef.StartCoroutine(inventoryRef.DisplayKeyMissing());
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ExitGate"))
        {
            //false wenn Player den Collider verlässt
            playerCollidingWithExitGate = false;
        }
    }

    #region SetOnAction

    public void SetOnPlayerMove(Action _newFunc)
    {
        OnPlayerMove += _newFunc;
    }
    public void SetOnPlayerSprint(Action _newFunc)
    {
        OnPlayerSprint += _newFunc;
    }
    public void SetOnPlayerIdle(Action _newFunc)
    {
        OnPlayerIdle += _newFunc;
    }
    public void SetOnPlayerJump(Action _newFunc)
    {
        OnPlayerJump += _newFunc;
    }
    public void SetOnPlayerHit(Action _newFunc)
    {
        OnPlayerHit += _newFunc;
    }
    public void SetOnPlayerResetEndurance(Action _newFunc)
    {
        OnPlayerResetEndurance += _newFunc;
    }
    public void SetOnPlayerResetEnduranceCompleted(Action _newFunc)
    {
        OnPlayerResetEnduranceCompleted += _newFunc;
    }  
    public void SetOnPlayerEnduranceLimitReached(Action _newFunc)
    {
        OnPlayerEnduranceLimitReached += _newFunc;
    }
    #endregion
}
