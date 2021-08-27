using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: footstepAudio.cs
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
 *  09.07.2021  RK  erstellt
 *  
 *****************************************************************************/
public class PlayerFootstepsSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClipWalk;
    [SerializeField]
    private AudioClip audioClipRun;
    [SerializeField]
    private AudioSource footstepAudio;
    [SerializeField]
    private float stepTimeWalk = 0.4f;
    [SerializeField]
    private float stepTimeRun = 0.2f;

    private float dealy = 0;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerController.SetOnPlayerMove(FootSteps);
        playerController.SetOnPlayerSprint(FootStepsRun);

    }

    /// <summary>
    /// Spiel SFX für Gehen ab
    /// </summary>
    void FootSteps()
    {
        if (dealy >= stepTimeWalk)
        {
            footstepAudio.clip = audioClipWalk;
            footstepAudio.Play();
            dealy = 0f;
        }

        dealy += Time.deltaTime;
    }

    /// <summary>
    /// Spiel SFX für Sprinten ab
    /// </summary>
    void FootStepsRun()
    {
        if (dealy >= stepTimeRun)
        {
            footstepAudio.clip = audioClipRun;
            footstepAudio.Play();
            dealy = 0f;
        }

        dealy += Time.deltaTime;
    }
}
