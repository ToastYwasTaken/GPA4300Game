using System.Collections;
using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: SearchPlayerAI.cs
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
 *  16.06.2021  RK  Created
 *  
 *****************************************************************************/
public class SearchPlayerAI : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    [SerializeField]
    private float fieldOfView = 120f;
    [SerializeField]
    private float viewOffest = 0.5f;
    [SerializeField]
    private float viewMaxDistance = 5f;
    [SerializeField]
    private float searchingTime = 0.5f;
    [SerializeField]
    private float repeatRate = 0.5f;

    [SerializeField]
    private float WaitTimeBeforeStopSearching = 5f;

    public bool isPlayerSprints = false;
    public bool isPlayerDetected = false;


    private bool coroutineIsActive = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    private void Start()
    {
        StartSearchingPlayer();
    }

    public void StartSearchingPlayer()
    {
        if (player)
        {
            isPlayerDetected = false;
            InvokeRepeating(nameof(SearchingPlayer), searchingTime, repeatRate);
        }
        else
        {
            Debug.LogError("Kein Spieler gefunden!");
        }
    }

    private void SearchingPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        Ray ray = new Ray(transform.position, direction.normalized);

        Debug.DrawRay(transform.position, direction, Color.green);

        // Prüfen ob, der Spieler gerade sprintet
        isPlayerSprints = playerController.playerSprints;

        if (Physics.Raycast(ray, out RaycastHit raycast, viewMaxDistance))
        {
            if (raycast.collider.gameObject == player)
            {
                Vector3 rayDirection = raycast.transform.position - transform.position;
                float angle = Vector3.Angle(rayDirection, transform.forward);

                if (angle < fieldOfView * viewOffest)
                {
                    coroutineIsActive = false;
                    isPlayerDetected = true;
                }
                else if (isPlayerSprints && !isPlayerDetected)
                {
                    coroutineIsActive = false;
                    isPlayerDetected = true;
                }

            }
            else
            {
                if (!coroutineIsActive && isPlayerDetected)
                {
                    StartCoroutine(PlayerNotVisible());
                }
                //isPlayerDetected = false;
            }
        }
    }

    IEnumerator PlayerNotVisible()
    {
        coroutineIsActive = true;
        yield return new WaitForSecondsRealtime(WaitTimeBeforeStopSearching);
        isPlayerDetected = false;
        coroutineIsActive = false;
        StopCoroutine(PlayerNotVisible());
    }

    public void StopSearchingPlayer()
    {
        CancelInvoke(nameof(SearchingPlayer));
    }
}
