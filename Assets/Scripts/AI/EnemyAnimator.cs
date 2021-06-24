using UnityEngine;

/******************************************************************************
 * Project: GPA4300Game
 * File: EnemyAnimator.cs
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
 *  21.06.2021  RK  Created
 *  
 *****************************************************************************/
public class EnemyAnimator : MonoBehaviour
{
    Animator mAnim;

    private void Awake()
    {
        mAnim = GetComponent<Animator>();
    }

    public void PlayIdleAnimation(bool _value)
    {
        mAnim.SetBool("idle_normal", _value);
    }

    public void PlayIdleCombatAnimation(bool _value)
    {
        mAnim.SetBool("idle_combat", _value);
    }

    public void PlayMoveAnimation(bool _value)
    {
        mAnim.SetBool("move_forward", _value);
    }

    public void PlayRunAnimation(bool _value)
    {
        mAnim.SetBool("move_forward_fast", _value);
    }

    public void TriggerAttack()
    {
        mAnim.SetTrigger("attack_short_001");
    }

    public void PlayDamageAnimation(bool _value)
    {
        mAnim.SetBool("damage_001", _value);
    }

    public void PlayDeadAnimation(bool _value)
    {
        mAnim.SetBool("dead", _value);
    }


}