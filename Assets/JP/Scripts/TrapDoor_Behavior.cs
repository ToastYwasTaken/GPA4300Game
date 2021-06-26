using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor_Behavior : MonoBehaviour
{
    [SerializeField]
    private Animator trapDoorAnimator;


    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            trapDoorAnimator.SetTrigger("triggered");
        }
    }
}
