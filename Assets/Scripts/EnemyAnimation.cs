using UnityEngine;
/******************************************************************************
 * Project: GPA4300Game
 * File: EnemyAnimation.cs
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
 *  18.06.2021  RK  Created
 *  
 *****************************************************************************/
public class EnemyAnimation : MonoBehaviour
{
    private Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        if (anim)
        {
            Debug.Log("Animation found!");
        }
    }

    public void PlayIdleAnimation()
    {
        if (CheckCurrentAnimation("idle_normal")) return;

        anim.CrossFade("idle_normal", 0.0f);
        anim.CrossFadeQueued("idle_normal");
    }

    public void PlayIdleCombatAnimation()
    {

        if (CheckCurrentAnimation("idle_combat")) return;

        anim.CrossFade("idle_combat", 0.0f);
        anim.CrossFadeQueued("idle_normal");
    }

    public void PlayMoveAnimation()
    {
        if (CheckCurrentAnimation("move_forward_fast"))
        {
            anim.CrossFade("move_forward_fast");
        }
        else
        {
            anim.Play("move_forward");
        }
    }

    public void PlayRunAnimation()
    {
        //if (anim.IsPlaying("move_forward"))
        //{
        //    anim.CrossFade("idle_normal", 0.3f);
        //}

        //if (anim.IsPlaying("move_forward_fast"))
        //{
        //    anim.CrossFade("idle_normal", 0.5f);
        //}

        if (CheckCurrentAnimation("move_forward"))
        {
            anim.CrossFade("move_forward");
        }
        else
        {
            anim.Play("move_forward_fast");
        }
    }

    public void PlayAttackAnimation()
    {
        if (CheckCurrentAnimation("attack_short_001")) return;


        anim.CrossFade("attack_short_001", 0.0f);
        anim.CrossFadeQueued("idle_combat");
    }

    public void PlayDamageAnimation()
    {
        if (CheckCurrentAnimation("damage_001")) return;

        anim.CrossFade("damage_001", 0.0f);
        anim.CrossFadeQueued("idle_combat");
    }

    public void PlayDeadAnimation()
    {
        if (CheckCurrentAnimation("dead")) return;

        anim.CrossFade("dead", 0.2f);
    }

    private bool CheckCurrentAnimation(string _clipname)
    {
        return anim.GetClip(_clipname) ? false : true;
    }


}
