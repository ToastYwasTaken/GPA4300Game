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
    private Rigidbody PlayerBody;
    [SerializeField]
    private bool SprintActive = true;
    [SerializeField]
    private bool JumpActive = false;
    [SerializeField]
    private bool RotatePlayerWithButtons = false;

    public Vector3 StartPosition;
    public Transform CamTransform;

    public float MoveSpeed = 5f;
    [Tooltip("Sprint Speed = Move Speed * Speed Multiplier")]
    public float SpeedMultiplier = 2f;
    public float RotateSpeed = 200f;
    public float FallingDownLimit = -5f;
    public float JumpForce = 5f;
    public bool groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        PlayerBody = GetComponent<Rigidbody>();
        PlayerBody.transform.position = StartPosition;
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
        if (transform.position.y <= FallingDownLimit)
        {
            PlayerBody.transform.position = StartPosition;
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
        if (Input.GetButton("Run") && SprintActive)
        {
            speed = MoveSpeed * SpeedMultiplier;
        }
        else
        {
            speed = MoveSpeed;
        }

        // Mit der Tastatur drehen
        if (RotatePlayerWithButtons)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, keyInput.x * RotateSpeed * Time.deltaTime, 0);
            PlayerBody.MoveRotation(PlayerBody.rotation * deltaRotation);
        }
        else
        {
            // Zur Seite gehen
            PlayerBody.MovePosition(PlayerBody.position + speed * Time.deltaTime * keyInput.x * transform.right);
        }

        // Nach vorne gehen
        PlayerBody.MovePosition(PlayerBody.position + speed * Time.deltaTime * keyInput.z * transform.forward);
    }

    /// <summary>
    /// Maus Steuerung
    /// </summary>
    void PlayerRotating()
    {
        Vector3 mouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        // Spieler sieht nach oben oder unten
        Quaternion deltaRotationY = Quaternion.Euler(RotateSpeed * Time.deltaTime * -mouseInput.y, 0, 0);
        CamTransform.Rotate(deltaRotationY.eulerAngles);

        // Spieler dreht sich nach links oder rechts
        Quaternion deltaRotationX = Quaternion.Euler(0, RotateSpeed * Time.deltaTime * mouseInput.x, 0);
        PlayerBody.MoveRotation(PlayerBody.rotation * deltaRotationX);

    }

    /// <summary>
    /// Springen
    /// </summary>
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && groundCheck && JumpActive)
        {
            PlayerBody.AddForceAtPosition(transform.up * JumpForce, transform.position, ForceMode.Impulse);
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
