using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerSounds.cs
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
public class PlayerSounds : MonoBehaviour
{
    public AudioClip audioClipWalk;
    public AudioClip audioClipRun;
    [SerializeField]
    private float stepTimeWalk = 0.4f;
    [SerializeField]
    private float stepTimeRun = 0.2f;
    [SerializeField]
    private float volume = 1f;

    private float dealy = 0;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerController.SetOnPlayerMove(FootSteps);
        playerController.SetOnPlayerMoveRun(FootStepsRun);
    }

    void FootSteps()
    {
        if (dealy >= stepTimeWalk)
        {
            AudioSource.PlayClipAtPoint(audioClipWalk, transform.position, volume);
            dealy = 0f;
        }

        dealy += Time.deltaTime;
    }

    void FootStepsRun()
    {
        if (dealy >= stepTimeRun)
        {
            AudioSource.PlayClipAtPoint(audioClipRun, transform.position, volume);
            dealy = 0f;
        }

        dealy += Time.deltaTime;
    }
}
