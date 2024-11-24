using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: SectorManager.cs
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
 *  15.08.2021  RK  erstellt
 * 
 *  
 *****************************************************************************/
public class SwitchEyes : MonoBehaviour
{
    [SerializeField]
    Light eyeLeft, eyeRight;
    [SerializeField]
    Light combatEyeLeft, combatEyeRight;
    [SerializeField]
    Light auraLight;
    [SerializeField]
    Light combatAuraLight;

    private EnemyAI enemyAI;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.SetOnPatrol(SwitchEyeToDefault);
        enemyAI.SetOnSeePlayer(SwitchEyeToComabt);
    }

    /// <summary>
    /// Enemy leuchtet in der Default Farbe (blau)
    /// </summary>
    private void SwitchEyeToDefault()
    {
        eyeLeft.enabled = true;
        eyeRight.enabled = true;
        combatEyeLeft.enabled = false;
        combatEyeRight.enabled = false;
    }

    /// <summary>
    /// Enemy leuchtet in der Combat Farbe (rot)
    /// </summary>
    private void SwitchEyeToComabt()
    {
        eyeLeft.enabled = false;
        eyeRight.enabled = false;
        combatEyeLeft.enabled = true;
        combatEyeRight.enabled = true;
    }



}
