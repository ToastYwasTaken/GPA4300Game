using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerController.cs
 * Version: 1.01
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
 *  11.06.2021  RK  Created
 *  15.06.2021  RK  Added max vertical Camera angle
 *  22.06.2021  FM  Added check if the game is paused
 *  24.06.2021  FM  Added changing sensitivity in option menu, therefore this script was adjusted
 *  26.06.2021  RK  Modified PlayerMovement -> don't sprint backwards 
 *  28.06.2021  RK  Added PlayerAnimator (Animations)
 *  30.06.2021  RK  Added Collision Check Wall
 *  
 *****************************************************************************/

/*NOTES
 * Idle     0,  0.611,  0.235
 * Walk     0,  0.560,  0.280
 * Sprint   0,  0.210,  0.470
 * 
 * Fehler: Spieler l�uft weiter nach vorne obwohl keine Taste gedr�ckt wird 
 * - Animation bereits ausgeschlossen
 * - Das Drehen mit den Tasten ist davon nicht betroffen
 *
 */

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerBody;
    private PlayerAnimator playerAnimator;

    public Vector3 startPosition;
    private Vector3 positionIdleCam = new Vector3(0f, 0.611f, 0.235f);
    private Vector3 positionWalkCam = new Vector3(0f, 0.560f, 0.235f);
    private Vector3 positionSprintCam = new Vector3(0f, 0.175f, 0.53f);
    public Transform camTransform;

    [SerializeField]
    private bool sprintActive = true;

    [Tooltip("Sprint Speed")]
    public float speedMultiplier = 2f;

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
    public float fallingDownLimit = -5f;
    public float sensitivityMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();
        playerBody.transform.position = startPosition /*new Vector3(108, 2, 60)*/;
        playerAnimator.PlayIdleAnimation(true);
       
       // camTransform.transform.localPosition = positionIdleCam;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GUIOptionMenu.isPaused) //verhindert movement wenn das Spiel pausiert ist
        {
            sensitivityMultiplier = PlayerPrefs.GetFloat("sensitivity");
            //Debug.Log("sensitivity Mult: " + sensitivityMultiplier);
            // Verhindert das der Player unendlich f�llt
            FallingDownCheck();

            // Springen
            Jump();
        }

    }

    private void FixedUpdate()
    {
        if (!GUIOptionMenu.isPaused) //verhindert movement wenn das Spiel pausiert ist
        {
            PlayerRotating();
            PlayerMovement();
        }
       // Debug.Log(camTransform.position);
    }

    /// <summary>
    /// Setzt den Spieler zu Startposition zur�ck, wenn er f�llt
    /// </summary>
    void FallingDownCheck()
    {
        if (transform.position.y <= fallingDownLimit)
        {
            playerBody.transform.position = startPosition;
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
            camTransform.transform.localPosition = positionIdleCam;
   
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
                camTransform.transform.localPosition = positionSprintCam;
                speed = moveSpeed * speedMultiplier;

                
            }
            else 
            {
                playerAnimator.PlayWalkAnimation(true); // true
                playerAnimator.PlayIdleAnimation(false);
                playerAnimator.PlaySprintAnimation(false);
                camTransform.transform.localPosition = positionIdleCam;
               // camTransform.transform.localPosition = positionWalkCam;
                speed = moveSpeed;
            
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
        cameraAngle += -mouseInput.y * rotationSpeed * sensitivityMultiplier * Time.deltaTime;

        // Winkel der Kamera auf min und max begrenzen
        cameraAngle = Mathf.Clamp(cameraAngle, -maxVerticalCameraAngle, maxVerticalCameraAngle);
        // Debug.Log(cameraAngle);

        // Rotationswinkel zuweisen
        camTransform.transform.localEulerAngles = new Vector3(cameraAngle, 0, 0);

        // Spieler dreht sich nach links oder rechts
        Quaternion deltaRotationX = Quaternion.Euler(0, rotationSpeed * sensitivityMultiplier * Time.deltaTime * mouseInput.x, 0);
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
            playerBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Ladung
            isGrounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            hitWall = true;
        else
            hitWall = false;
    }
}
