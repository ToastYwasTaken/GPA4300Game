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
    private Animator anim;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();

        playerController.SetOnPlayerIdle(PlayPlayerIdle);
        playerController.SetOnPlayerMove(PlayPlayerWalk);
        playerController.SetOnPlayerMoveRun(PlayerPlayerRun);
        playerController.SetOnPlayerJump(PlayPlayerJump);
        playerController.SetOnPlayerHit(PlayPlayerHit);

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
        // TODO Animation wenn der Spieler getroffen wurde aktivieren
    }


    public void PlayIdleAnimation(bool _value)
    {
        anim.SetBool("Player Idle", _value);
    }

    public void PlayWalkAnimation(bool _value)
    {
        anim.SetBool("Player Walk", _value);
    }

    public void PlaySprintAnimation(bool _value)
    {
        anim.SetBool("Player Sprint", _value);
    }

    public void TriggerPlayerJump()
    {
        anim.SetTrigger("Player Jump");
    }
}
