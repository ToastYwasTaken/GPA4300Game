using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Regelt das Verhalten von HiddenDoor mit Bezug zum entsprechenden Schalter
 * Author: JP
 */
public class HiddenDoor_Behavior : MonoBehaviour
{
    [SerializeField]
    private Animator hiddenDoorAnimator;

    [SerializeField]
    private Animator switchAnimator;    // hier wird der Animator des Schalters hineingezogen,
                                        // mit dem sich die Tür bedienen lassen soll

    [SerializeField]
    private AudioSource doorAudio;

    // Tür verhällt sich relativ zum Schalter....
    private void Update()
    {
        if (switchAnimator.GetBool("switchOn") == true)     //...Steht der Schalter auf an,...
        {
            hiddenDoorAnimator.SetBool("doorOpen", true);   //...ist die Tür auf....
            doorAudio.Play();
        }
        if (switchAnimator.GetBool("switchOn") == false)    //...Steh der Schalter auf aus,...
        {
            hiddenDoorAnimator.SetBool("doorOpen", false);  //...ist die Tür zu.
            doorAudio.Play();
        }
    }

    //private void OnTriggerEnter(Collider _other)
    //{
    //    if (_other.gameObject.tag == "Player")
    //    {
    //        hiddenDoorAnimator.SetTrigger("triggered");
    //    }
    //}
}
