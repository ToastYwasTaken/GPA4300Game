using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Regelt das Verhalten der Falle TrapDoor
 * Author: JP
 */
public class TrapDoor_Behavior : MonoBehaviour
{
    [SerializeField]
    private Animator trapDoorAnimator;


    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            PlayerController.SprintActive = false;      //Damit der Spieler nicht über die Falle "hinwegsprinten" kann

            trapDoorAnimator.SetTrigger("triggered");
        }
    }
}
