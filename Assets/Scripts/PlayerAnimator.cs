using UnityEngine;
/******************************************************************************
 * Project: GPA4300Game
 * File: PlayerAnimator.cs
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
 *  28.06.2021  RK  erstellt
 *  21.08.2021  RK  PlayerController Referenz hinzugefügt
 *              RK  Methoden im PlayerController registieren
 *  
 *****************************************************************************/
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animChar;
    [SerializeField]
    private Animator animCam;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();


        playerController.SetOnCamIdle(PlayCamIdle);
        playerController.SetOnCamSprint(PlayCamSprint);

        playerController.SetOnPlayerIdle(PlayPlayerIdle);
        playerController.SetOnPlayerMove(PlayPlayerWalk);
        playerController.SetOnPlayerMoveRun(PlayerPlayerRun);
        playerController.SetOnPlayerJump(PlayPlayerJump);
        playerController.SetOnPlayerHit(PlayPlayerHit);

    }

    private void PlayCamIdle()
    {
        PlayCamIdleAnimation(true);
        PlayCamSprinteAnimation(false);
    }

    private void PlayCamSprint()
    {
        PlayCamIdleAnimation(false);
        PlayCamSprinteAnimation(true);
    }

    private void PlayPlayerIdle()
    {
        PlaySprintAnimation(false);
        PlayWalkAnimation(false);
        PlayIdleAnimation(true);
    }

    private void PlayPlayerWalk()
    {
        PlayWalkAnimation(true);
        PlayIdleAnimation(false);
        PlaySprintAnimation(false);
    }

    private void PlayerPlayerRun()
    {
        PlayWalkAnimation(false);
        PlayIdleAnimation(false);
        PlaySprintAnimation(true);
    }

    private void PlayPlayerJump()
    {
        TriggerPlayerJump();
    }

    private void PlayPlayerHit()
    {
        TriggerPlayerTakeDamage();
    }

    private void PlayCamIdleAnimation(bool _value)
    {
        animCam.SetBool("CamIdle", _value);
    }

    private void PlayCamSprinteAnimation(bool _value)
    {
        animCam.SetBool("CamSprint", _value);
    }


    private void PlayIdleAnimation(bool _value)
    {
        animChar.SetBool("Player Idle", _value);
    }

    private void PlayWalkAnimation(bool _value)
    {
        animChar.SetBool("Player Walk", _value);
    }

    private void PlaySprintAnimation(bool _value)
    {
        animChar.SetBool("Player Sprint", _value);
    }

    private void TriggerPlayerJump()
    {
        animChar.SetTrigger("Player Jump");
    }

    private void TriggerPlayerTakeDamage()
    {
        animChar.SetTrigger("Player Damaged");
    }

   
}
