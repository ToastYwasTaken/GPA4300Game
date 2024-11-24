using UnityEngine;

/******************************************************************************
    * Project: GPA4300Game
    * File: KIActiveTrigger.cs
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
    *  26.08.2021  RK  erstellt
    *  
    *****************************************************************************/
public class KIActiveTrigger : MonoBehaviour
{
    [SerializeField]
    private EnemyAI enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        enemyAI = FindObjectOfType<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyAI)
        {
            // Aktiviert die KI, wenn der Player durchlaeuft
            if (other.gameObject.CompareTag("Player"))
            {
                enemyAI.CanRunning = true;
            }
        }
    }
}
