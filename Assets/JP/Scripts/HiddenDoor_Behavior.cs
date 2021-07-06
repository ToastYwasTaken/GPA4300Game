using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoor_Behavior : MonoBehaviour
{
    [SerializeField]
    private Animator hiddenDoorAnimator;

    [SerializeField]
    private Animator switchAnimator;

    private void Update()
    {
        if (switchAnimator.GetBool("switchOn") == true)
        {
            hiddenDoorAnimator.SetBool("doorOpen", true);
        }
        if (switchAnimator.GetBool("switchOn") == false)
        {
            hiddenDoorAnimator.SetBool("doorOpen", false);
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
