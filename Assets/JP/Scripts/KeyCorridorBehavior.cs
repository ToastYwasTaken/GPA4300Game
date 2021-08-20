using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCorridorBehavior : MonoBehaviour
{
    [SerializeField]
    private Animator corridorAnimator;
    [SerializeField]
    private GameObject key;

    [SerializeField]
    private GameObject leftWall;
    [SerializeField]
    private GameObject rightWall;
    public int tagchangeAfterSeconds = 5;

    private IEnumerator TagWallsAsTrap()
    {
        yield return new WaitForSeconds(tagchangeAfterSeconds);

        leftWall.gameObject.tag = "Trap";
        rightWall.gameObject.tag = "Trap";
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(TagWallsAsTrap());
                Destroy(key);
                corridorAnimator.SetTrigger("triggered");
            }
        }
    }
}
