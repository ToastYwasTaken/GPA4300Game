using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Behavior : MonoBehaviour
{
    [SerializeField]
    private Transform otherPortalSpawner;
    private static bool portalReady = true;
    private int portalRefreshTime = 5;

    private IEnumerator RefreshPortal()
    {
        yield return new WaitForSeconds(portalRefreshTime);
        portalReady = true;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player" && portalReady == true)
        {
            portalReady = false;
            _other.transform.position = otherPortalSpawner.transform.position;
            StartCoroutine(RefreshPortal());
        }
    }
}
