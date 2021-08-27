using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************************
    * Project: GPA4300Game
    * File: GateAudio.cs
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
    *  20.08.2021  RK  erstellt
    *  
    *****************************************************************************/
public class GateAudio : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource audioSource = animator.GetComponent<AudioSource>();

        // Abspielen des Fallgitter Sounds
        audioSource.Play();


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
