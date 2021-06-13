using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerController.cs
 * Version: 1.0
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
 *  
 *****************************************************************************/

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerBody;
    [SerializeField]
    private bool sprintActive = true;
    [SerializeField]
    private bool jumpActive = false;
    [SerializeField]
    private bool rotatePlayerWithButtons = false;

    public Vector3 startPosition;
    public Transform camTransform;

    public float moveSpeed = 5f;
    [Tooltip("Sprint Speed = Move Speed * Speed Multiplier")]
    public float speedMultiplier = 2f;
    public float rotatingSpeed = 200f;
    public float fallingDownLimit = -5f;
    public float jumpForce = 5f;
    public bool groundCheck;

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

        Jump();
    }

    private void FixedUpdate()
    {
        PlayerRotating();
        PlayerMovement();
    }

    /// <summary>
    /// Setzt den Spieler zu Start Position zurück, wenn er fällt
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
            Quaternion deltaRotation = Quaternion.Euler(0, keyInput.x * rotatingSpeed * Time.deltaTime, 0);
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
        Quaternion deltaRotationY = Quaternion.Euler(rotatingSpeed * Time.deltaTime * -mouseInput.y, 0, 0);
        camTransform.Rotate(deltaRotationY.eulerAngles);

        // Spieler dreht sich nach links oder rechts
        Quaternion deltaRotationX = Quaternion.Euler(0, rotatingSpeed * Time.deltaTime * mouseInput.x, 0);
        playerBody.MoveRotation(playerBody.rotation * deltaRotationX);

    }

    /// <summary>
    /// Springen
    /// </summary>
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && groundCheck && jumpActive)
        {
            playerBody.AddForceAtPosition(transform.up * jumpForce, transform.position, ForceMode.Impulse);
            groundCheck = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = true;
        }
    }

}
