using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Lässt den Spieler den Schalter mit der Taste 'E' bedienen
 * Author: JP
 */
public class SwitchBehavior_Skript : MonoBehaviour
{
    [SerializeField]
    private Animator switchAnimator;
    [SerializeField]
    private AudioSource switchAudio;

    private void OnTriggerStay(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                switchAnimator.SetBool("switchOn", !switchAnimator.GetBool("switchOn"));
                switchAudio.Play();
            }
        }
    }
}
