using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_Behavior : MonoBehaviour
{
    [SerializeField]
    private Animator RatAnimator;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            RatAnimator.SetTrigger("triggered");
        }
    }
}
