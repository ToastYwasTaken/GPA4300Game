using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoor_Behavior : MonoBehaviour
{
    [SerializeField]
    private Animator hiddenDoorAnimator;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            hiddenDoorAnimator.SetTrigger("triggered");
        }
    }
}
