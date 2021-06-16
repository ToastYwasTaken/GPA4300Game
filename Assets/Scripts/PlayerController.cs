using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerController.cs
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
 *  11.06.2021  RK  Created
 *  15.06.2021  RK  Added max vertical Camera angle
 *  
 *****************************************************************************/

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerBody;

    public Vector3 startPosition;
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
    [SerializeField]
    private bool rotatePlayerWithButtons = false;
    [SerializeField]

    private float maxVerticalCameraAngle = 45f;
    private float cameraAngle = 0f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public float fallingDownLimit = -5f;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        playerBody.transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Verhindert das der Player unendlich fällt
        FallingDownCheck();
        // Springen
        Jump();
    }

    private void FixedUpdate()
    {
        PlayerRotating();
        PlayerMovement();
    }

    /// <summary>
    /// Setzt den Spieler zu Startposition zurück, wenn er fällt
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

        // Bewegungsgeschwindigkeit
        float speed;

        // Sprint
        if (Input.GetButton("Run") && sprintActive)
        {
            speed = moveSpeed * speedMultiplier;
        }
        else
        {
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

    /// <summary>
    /// Maus Steuerung
    /// </summary>
    void PlayerRotating()
    {
        Vector3 mouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        // Spieler sieht nach oben oder unten
        cameraAngle += -mouseInput.y * rotationSpeed * Time.deltaTime;

        // Winkel der Kamera auf min und max begrenzen
        cameraAngle = Mathf.Clamp(cameraAngle, -maxVerticalCameraAngle, maxVerticalCameraAngle);
        // Debug.Log(cameraAngle);

        // Rotationswinkel zuweisen
        camTransform.transform.localEulerAngles = new Vector3(cameraAngle, 0, 0);

        // Spieler dreht sich nach links oder rechts
        Quaternion deltaRotationX = Quaternion.Euler(0, rotationSpeed * Time.deltaTime * mouseInput.x, 0);
        playerBody.MoveRotation(playerBody.rotation * deltaRotationX);
    }

    /// <summary>
    /// Springen
    /// </summary>
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && jumpActive)
        {
            playerBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

}
