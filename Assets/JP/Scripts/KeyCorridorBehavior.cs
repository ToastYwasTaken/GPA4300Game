using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Sobald der Spieler den Schlüssel aufhebt, fangen die Wände an, sich aufeinander zuzubewegen
 * und zerquetschen den Spieler, wenn er nicht schnell genug ist.
 */
public class KeyCorridorBehavior : MonoBehaviour
{
    [SerializeField]
    private Animator corridorAnimator;
    [SerializeField]
    private AudioSource corridorAudio;

    [SerializeField]
    private GameObject key;

    [SerializeField]
    private GameObject leftWall;
    [SerializeField]
    private GameObject rightWall;
    private int tagchangeAfterSeconds = 5;

    private IEnumerator TagWallsAsTrap()
    {
        yield return new WaitForSeconds(tagchangeAfterSeconds);
        //Nachdem sich die Wände einige Sekunden aufeinander zubewegen, wechset ihr Tag auf "Trap"
        //--> Instant Death bei berührung
        leftWall.gameObject.tag = "Trap";
        rightWall.gameObject.tag = "Trap";
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))                //Sobald der Spieler E drückt...
            {
                Destroy(key);                               //nimmt er den Schlüssel auf...
                corridorAnimator.SetTrigger("triggered");   //...und die Wände fangen an sich aufeinander zuzubewegen
                corridorAudio.Play();
                StartCoroutine(TagWallsAsTrap());           //Berührung mit den Wänden wird tödlich
            }
        }
    }
}
