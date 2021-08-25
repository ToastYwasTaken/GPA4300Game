using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Sobald der Spieler den Schl�ssel aufhebt, fangen die W�nde an, sich aufeinander zuzubewegen
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
        //Nachdem sich die W�nde einige Sekunden aufeinander zubewegen, wechset ihr Tag auf "Trap"
        //--> Instant Death bei ber�hrung
        leftWall.gameObject.tag = "Trap";
        rightWall.gameObject.tag = "Trap";
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))                //Sobald der Spieler E dr�ckt...
            {
                Destroy(key);                               //nimmt er den Schl�ssel auf...
                corridorAnimator.SetTrigger("triggered");   //...und die W�nde fangen an sich aufeinander zuzubewegen
                corridorAudio.Play();
                StartCoroutine(TagWallsAsTrap());           //Ber�hrung mit den W�nden wird t�dlich
            }
        }
    }
}
