using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Regelt das Verhalten der beiden Portale
 * Author: JP
 */
public class Portal_Behavior : MonoBehaviour
{
    [SerializeField]
    private Animator thisPortalAnimator;
    [SerializeField]
    private Transform otherPortalSpawner;
    [SerializeField]
    private Animator otherPortalAnimator;

    private int beamWaitingTime = 2;

    private static bool portalReady = true;
    private int portalRefreshTime = 7;

    //Portal soll warten bis die erste Animation abgespielt ist
    //und erst dann beamen
    private IEnumerator WaitThanBeam(Collider _other)
    {
        yield return new WaitForSeconds(beamWaitingTime);
        _other.transform.position = otherPortalSpawner.transform.position;
    }
    //nach dem Beamen, braucht das Portal eine Zeit, bis es wieder einsatzfähig ist
    private IEnumerator RefreshPortal()
    {
        yield return new WaitForSeconds(portalRefreshTime);
        portalReady = true;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player" && portalReady == true)
        {
            thisPortalAnimator.SetTrigger("beam");
            otherPortalAnimator.SetTrigger("beam");
            StartCoroutine(WaitThanBeam(_other));

            portalReady = false;
            StartCoroutine(RefreshPortal());
        }
    }
}
