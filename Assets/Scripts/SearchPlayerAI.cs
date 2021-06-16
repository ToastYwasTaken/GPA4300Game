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
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float fieldOfView = 120f;
    [SerializeField]
    private float ViewOffest = 0.5f;
    [SerializeField]
    private float layerMaskValue = 5f;
    [SerializeField]
    private float searchingTime = 0.5f;
    [SerializeField]
    private float repeatRate = 0.5f;
    [SerializeField]
    private float distanceToThePlayer = 2.7f;

    public bool isPlayerDetected = false;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (isPlayerDetected)
        {
            FollowPlayer();
        }
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
        
        if (Physics.Raycast(ray, out RaycastHit raycast, layerMaskValue))
        {
            if (raycast.collider.gameObject  == player)
            {
                Vector3 rayDirection = raycast.transform.position - transform.position;
                float angle = Vector3.Angle(rayDirection, transform.forward);
                if (angle < fieldOfView * ViewOffest)
                {
                    StopSearchingPlayer();
                }
            }
        }
    }

    private void FollowPlayer()
    {
        GetComponent<EnemyAI>().SetDestination(player, distanceToThePlayer);
    }

    public void StopSearchingPlayer()
    {
        isPlayerDetected = true;
        CancelInvoke(nameof(SearchingPlayer));
    }
}
