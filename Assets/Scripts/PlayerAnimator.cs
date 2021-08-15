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
 *  
 *****************************************************************************/
public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
