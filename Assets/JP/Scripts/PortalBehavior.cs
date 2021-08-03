using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehavior : MonoBehaviour
{
    [SerializeField]
    private Animator thisPortalAnimator;
    [SerializeField]
    private Animator otherPortalAnimator;
    [SerializeField]
    private Transform otherPortalSpawner;
    [SerializeField]
    private AudioSource portalAudio;

    private static bool portalReady = true;
    private int portalRefreshTime = 10;

    private IEnumerator RefreshPortal()
    {
        thisPortalAnimator.SetBool("portalReady", false);
        otherPortalAnimator.SetBool("portalReady", false);

        yield return new WaitForSeconds(portalRefreshTime);

        thisPortalAnimator.SetBool("portalReady", true);
        otherPortalAnimator.SetBool("portalReady", true);

        portalReady = true;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player" && portalReady == true)
        {
            portalAudio.Play();
            _other.transform.rotation = otherPortalSpawner.transform.rotation;
            _other.transform.position = otherPortalSpawner.transform.position;

            portalReady = false;
            StartCoroutine(RefreshPortal());
        }
    }
}
