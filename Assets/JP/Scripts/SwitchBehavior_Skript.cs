using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior_Skript : MonoBehaviour
{
    [SerializeField]
    private Animator switchAnimator;

    private void OnTriggerStay(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.KeypadEnter))
            {
                //switchAnimator.
            }
        }
    }
}
