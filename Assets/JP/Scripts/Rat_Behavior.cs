using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_Behavior : MonoBehaviour
{
    [SerializeField]
    private Animator ratAnimator;
    [SerializeField]
    private AudioSource ratAudio;

    //ratAudio wird nach dem ersten Mal abspielen ausgeschaltet, damit es nicht immer ausgelöst wird, wenn der Spieler in den BoxCollider läuft
    private IEnumerator DisableMouseAudio()
    {
        yield return new WaitForSeconds(2);
        ratAudio.enabled = false;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            ratAnimator.SetTrigger("triggered");
            ratAudio.Play();

            StartCoroutine(DisableMouseAudio()); 
        }
    }
}
