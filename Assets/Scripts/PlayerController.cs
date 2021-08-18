using System;
using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerController.cs
 * Version: 1.02
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
    private Rigidbody playerBody;
    private PlayerAnimator playerAnimator;
    //private Preferences preferences;

    private Action OnPlayerMove;
    private Action OnPlayerMoveRun;

    public Vector3 startPosition;

    [SerializeField]
    private Vector3 playerCurrentPosition;
    public Transform camTransform;

    [SerializeField]
    private static bool sprintActive = true;
    public static bool SprintActive
    {
        set { sprintActive = value; }
    }

    [Tooltip("Sprint Speed")]
    public float speedMultiplier = 2f;
    public bool playerSprints = false;

    [SerializeField]
    private bool jumpActive = false;
    public float jumpForce = 200f;

    [SerializeField]
    private bool isGrounded;
    private bool hitWall;
    [SerializeField]
    private bool rotatePlayerWithButtons = false;
    [SerializeField]
    private float maxVerticalCameraAngle = 45f;
    private float cameraAngle = 0f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public float fallingDownLimit = -10f;

    public bool PlayerCanMove { get; set; }
    public float Sensitivity { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();
        playerBody.transform.position = /*GameData.instance.PlayerPosition*/ new Vector3(90, 0, 40);
        playerAnimator.PlayIdleAnimation(true);

        PlayerCanMove = true;
        Sensitivity = GameData.instance.Sensitivity;

    }

    // Update is called once per frame
    void Update()
    {
        // Verhindert das der Player unendlich f�llt
        FallingDownCheck();

        // Springen
        Jump();

    }

    private void FixedUpdate()
    {

        PlayerRotating();
        if (PlayerCanMove)
            PlayerMovement();

    }

    /// <summary>
    /// Setzt den Spieler zu Startposition zur�ck, wenn er f�llt
    /// </summary>
    void FallingDownCheck()
    {
        if (transform.position.y <= fallingDownLimit)
        {
            playerBody.transform.position = GameData.instance.PlayerStartPosition;
            PlayerCanMove = true;
            sprintActive = true;
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
            playerAnimator.PlaySprintAnimation(false);
            playerAnimator.PlayWalkAnimation(false);
            playerAnimator.PlayIdleAnimation(true);

        }
        else
        {
            // Bewegungsgeschwindigkeit
            float speed;

            // Sprint
            if (Input.GetButton("Run") && sprintActive && keyInput.z > 0 && !hitWall) //default l shift
            {
                playerAnimator.PlayWalkAnimation(false);
                playerAnimator.PlayIdleAnimation(false);
                playerAnimator.PlaySprintAnimation(true); // true

                speed = moveSpeed * speedMultiplier;
                playerSprints = true;

                if (OnPlayerMoveRun != null)
                {
                    OnPlayerMoveRun.Invoke();
                }
            }
            else
            {
                playerAnimator.PlayWalkAnimation(true); // true
                playerAnimator.PlayIdleAnimation(false);
                playerAnimator.PlaySprintAnimation(false);

                speed = moveSpeed;
                playerSprints = false;

                if (OnPlayerMove != null)
                {
                    OnPlayerMove.Invoke();
                }
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
    /// Maus Steuerung
    /// </summary>
    void PlayerRotating()
    {
        Vector3 mouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        // Spieler sieht nach oben oder unten
        cameraAngle += -mouseInput.y * rotationSpeed * Sensitivity * Time.deltaTime;

        // Winkel der Kamera auf min und max begrenzen
        cameraAngle = Mathf.Clamp(cameraAngle, -maxVerticalCameraAngle, maxVerticalCameraAngle);
        // Debug.Log(cameraAngle);

        // Rotationswinkel zuweisen
        camTransform.transform.localEulerAngles = new Vector3(cameraAngle, 0, 0);

        // Spieler dreht sich nach links oder rechts
        Quaternion deltaRotationX = Quaternion.Euler(0, rotationSpeed * Sensitivity * Time.deltaTime * mouseInput.x, 0);
        playerBody.MoveRotation(playerBody.rotation * deltaRotationX);
    }

    /// <summary>
    /// Springen
    /// </summary>
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && jumpActive)
        {
            playerAnimator.TriggerPlayerJump();
            // Spring
            playerBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // TODO: Hit Animation abspielen
            Debug.Log("Player Hit!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Player befindet sich auf den Boden
            isGrounded = true;
        }
    }

    /// <summary>
    /// Verhindert das der Player durch Wände laufen kann
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            hitWall = true;
        else
            hitWall = false;
    }

    public void SetOnPlayerMove(Action _newFunc)
    {
        OnPlayerMove += _newFunc;
    }
    public void SetOnPlayerMoveRun(Action _newFunc)
    {
        OnPlayerMoveRun += _newFunc;
    }
}
